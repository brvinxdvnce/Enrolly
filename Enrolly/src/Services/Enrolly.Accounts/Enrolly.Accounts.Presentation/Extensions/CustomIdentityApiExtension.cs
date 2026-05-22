using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Encodings.Web;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Presentation.DTOs;
using Enrolly.Shared.Logging;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http.Metadata;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Enrolly.Accounts.Presentation.Extensions;

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/// <summary>
/// This is a custom extension that modifies the behavior of the
/// standard Microsoft.AspNetCore.Identity endpoints.
/// Except for the "/login" and "/refresh" paths,
/// which override the behavior for working with JWTs,
/// everything is standard.
/// </summary>

/// <summary>
/// Provides extension methods for <see cref="IEndpointRouteBuilder"/> to add identity endpoints.
/// </summary>
public static class CustomIdentityApiExtension
{
    // Validate the email address using DataAnnotations like the UserValidator does when RequireUniqueEmail = true.
    private static readonly EmailAddressAttribute _emailAddressAttribute = new();

    /// <summary>
    /// Uses standard ASP.NET Core Identity endpoints, modifying the "/login" "/refresh" routes behavior to use JWT.
    /// </summary>
    /// <typeparam name="TUser">The type describing the user. This should match the generic parameter in <see cref="UserManager{TUser}"/>.</typeparam>
    /// <param name="endpoints">
    /// The <see cref="IEndpointRouteBuilder"/> to add the identity endpoints to.
    /// Call <see cref="EndpointRouteBuilderExtensions.MapGroup(IEndpointRouteBuilder, string)"/> to add a prefix to all the endpoints.
    /// </param>
    /// <returns>An <see cref="IEndpointConventionBuilder"/> to further customize the added endpoints.</returns>
    public static IEndpointConventionBuilder MapCustomIdentityApiUsingJwt<TUser>(this IEndpointRouteBuilder endpoints)
        where TUser : class, new()
    {
        ArgumentNullException.ThrowIfNull(endpoints);

        /*var timeProvider = endpoints.ServiceProvider.GetRequiredService<TimeProvider>();
        var bearerTokenOptions = endpoints.ServiceProvider.GetRequiredService<IOptionsMonitor<BearerTokenOptions>>();
        */
        //var emailSender = endpoints.ServiceProvider.GetRequiredService<IEmailSender<TUser>>();
        var linkGenerator = endpoints.ServiceProvider.GetRequiredService<LinkGenerator>();

        // We'll figure out a unique endpoint name based on the final route pattern during endpoint generation.
        string? confirmEmailEndpointName = null;

        var routeGroup = endpoints.MapGroup("");

        // NOTE: We cannot inject UserManager<TUser> directly because the TUser generic parameter is currently unsupported by RDG.
        // https://github.com/dotnet/aspnetcore/issues/47338
        routeGroup.MapPost("/register", async Task<Results<Ok, ValidationProblem>>
            (
                [FromBody] UserRegisterRequestDto registration, 
                HttpContext context, 
                [FromServices] IServiceProvider sp
                ) => 
        {
            // если кто-то когда-то будет смотреть мой код и пытаться понять мой ход мысли - я вам сочувствую, 
            // поскольку я делаю это по приколу... я уже пожалел, что решился разобраться в кишках Identity
            // но в целом это прикольно... я еще я не знаю что там с Авторскими Правами, мб меня посадят
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var userStore = sp.GetRequiredService<IUserStore<User>>();
            var emailStore = (IUserEmailStore<User>)userStore;

            if (!userManager.SupportsUserEmail)
                throw new NotSupportedException($"{nameof(MapCustomIdentityApiUsingJwt)} requires a user store with email support.");
            
            if (string.IsNullOrEmpty(registration.Email) || !_emailAddressAttribute.IsValid(registration.Email))
                return CreateValidationProblem(IdentityResult.Failed(userManager.ErrorDescriber.InvalidEmail(registration.Email)));
            
            var user = new User();
            
            await userStore.SetUserNameAsync(user, registration.UserName ?? registration.Email, CancellationToken.None);
            await emailStore.SetEmailAsync(user, registration.Email, CancellationToken.None);
            user.PhoneNumber = registration.PhoneNumber;
            user.EmailConfirmed = true;
            
            var result = await userManager.CreateAsync(user, registration.Password!);
            
            if (!result.Succeeded)
                return CreateValidationProblem(result);

            /*await SendConfirmationEmailAsync(user, userManager, context, email);*/
            return TypedResults.Ok();
        });

        routeGroup.MapPost("/login", async Task<IResult>
            ([FromBody] LoginRequestDto login,
                UserManager<User> userManager,
                IOptions<JwtSettings> JwtSettings,
                IJwtProvider jwtProvider,
                HttpContext httpContext) =>
        {
            // на самом деле самое страшное в этом ковырянии в Identity - 
            // ожидание, пока откроются либы
            
            var jwtSettings = JwtSettings.Value;
            
            var user = await userManager.FindByEmailAsync(login.Email);

            if (user is null || !await userManager.CheckPasswordAsync(user, login.Password))
                return TypedResults.Problem("Invalid credentials", statusCode: StatusCodes.Status401Unauthorized);

            var newAccessToken = await jwtProvider.GenerateToken(user);
            
            var userRoles = await userManager.GetRolesAsync(user);
            
            var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            await userManager.SetAuthenticationTokenAsync(
                user, 
                jwtSettings.Issuer, 
                "RefreshToken",
                newRefreshToken);

            httpContext.Response.Cookies.Append("access-token", newAccessToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.ExpiresInHours),
                Path = "/"
            });
            
            httpContext.Response.Cookies.Append("refresh-token", newRefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(jwtSettings.RefreshExpiresInHours),
                Path = "/"
            });
            
            // я понимаю, что выдавать токены в явном виде - практика ужаснейшая.
            // если бы этот код использовался в "реальных условиях" - это стоило бы удалить 100%
            return TypedResults.Ok(new
            {
                userId = user.Id,
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            });
        });

        routeGroup.MapPost("/refresh", async Task<IResult> (
                [FromBody] RefreshTokenRequestDto refreshRequest, 
                UserManager<User> userManager,
                IJwtProvider jwtProvider,
                IOptions<JwtSettings> _jwtSettings) =>
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtSettings = _jwtSettings.Value;
            string userId = "";
            
            if (!jwtHandler.CanReadToken(refreshRequest.AccessToken))
                return TypedResults.Unauthorized();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtSettings.Issuer,
                ValidateIssuer = true,
                ValidAudience = jwtSettings.Audience,
                ValidateAudience = true,
                ValidateLifetime = false,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecurityKey))
            };
            
            try
            {
                var claimsPrincipal = jwtHandler.ValidateToken(
                    refreshRequest.AccessToken,
                    tokenValidationParameters, out _);
                
                userId = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new Exception();
            }
            catch (Exception ex)
            {
                return TypedResults.Unauthorized();
            }
            
            if (string.IsNullOrEmpty(userId))
                return TypedResults.Unauthorized();
            
            var user = await userManager.FindByIdAsync(userId);
            if (user is null) 
                return TypedResults.NotFound();
            
            if (await userManager.GetAuthenticationTokenAsync(
                    user, jwtSettings.Issuer, "RefreshToken") 
                != refreshRequest.RefreshToken)
                return TypedResults.Unauthorized();
            
            var newAccessToken = await jwtProvider.GenerateToken(user);

            var newRefreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            await userManager.SetAuthenticationTokenAsync(
                user, 
                jwtSettings.Issuer, 
                "RefreshToken",
                newRefreshToken);

            return TypedResults.Ok(new
            {
                accessToken = newAccessToken,
                refreshToken = newRefreshToken
            }); 
        });
        return new IdentityEndpointsConventionBuilder(routeGroup);
    }

    private static ValidationProblem CreateValidationProblem(string errorCode, string errorDescription) =>
        TypedResults.ValidationProblem(new Dictionary<string, string[]> {
            { errorCode, [errorDescription] }
        });

    private static ValidationProblem CreateValidationProblem(IdentityResult result)
    {
        // We expect a single error code and description in the normal case.
        // This could be golfed with GroupBy and ToDictionary, but perf! :P
        Debug.Assert(!result.Succeeded);
        var errorDictionary = new Dictionary<string, string[]>(1);

        foreach (var error in result.Errors)
        {
            string[] newDescriptions;

            if (errorDictionary.TryGetValue(error.Code, out var descriptions))
            {
                newDescriptions = new string[descriptions.Length + 1];
                Array.Copy(descriptions, newDescriptions, descriptions.Length);
                newDescriptions[descriptions.Length] = error.Description;
            }
            else
            {
                newDescriptions = [error.Description];
            }

            errorDictionary[error.Code] = newDescriptions;
        }

        return TypedResults.ValidationProblem(errorDictionary);
    }

    private static async Task<InfoResponse> CreateInfoResponseAsync<TUser>(TUser user, UserManager<TUser> userManager)
        where TUser : class
    {
        return new()
        {
            Email = await userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email."),
            IsEmailConfirmed = await userManager.IsEmailConfirmedAsync(user),
        };
    }

    // Wrap RouteGroupBuilder with a non-public type to avoid a potential future behavioral breaking change.
    private sealed class IdentityEndpointsConventionBuilder(RouteGroupBuilder inner) : IEndpointConventionBuilder
    {
        private IEndpointConventionBuilder InnerAsConventionBuilder => inner;

        public void Add(Action<EndpointBuilder> convention) => InnerAsConventionBuilder.Add(convention);
        public void Finally(Action<EndpointBuilder> finallyConvention) => InnerAsConventionBuilder.Finally(finallyConvention);
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class FromBodyAttribute : Attribute, IFromBodyMetadata
    {
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class FromServicesAttribute : Attribute, IFromServiceMetadata
    {
    }

    [AttributeUsage(AttributeTargets.Parameter)]
    private sealed class FromQueryAttribute : Attribute, IFromQueryMetadata
    {
        public string? Name => null;
    }
}
