using Enrolly.Admissions.Application.Authorization.Requirements;
using Enrolly.Admissions.Infrastructure.Authorization.Handlers;
using Enrolly.Auth.Authentication.Politics;
using Microsoft.AspNetCore.Authorization;

namespace Enrolly.Admissions.Presentation.Extensions;

public static class AuthRegistrationExtensions
{
    public static IServiceCollection AddAuthPolitics(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthPolicies.CaseFileEdit, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new OwnerOrManagerRequirement());
            });
        });
        
        return services;
    }

    public static IServiceCollection AddAuthHanders(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, OwnerOrManagerAdmissionEditHandler>();
        
        return services;
    }
}