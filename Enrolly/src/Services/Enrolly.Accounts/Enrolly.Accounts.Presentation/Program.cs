using Enrolly.Accounts.Application.Services.Implementations;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Infrastructure;
using Enrolly.Accounts.Infrastructure.Authorization.Handlers;
using Enrolly.Accounts.Infrastructure.Database;
using Enrolly.Accounts.Infrastructure.DependencyInjection;
using Enrolly.Accounts.Infrastructure.Seeders;
using Enrolly.Accounts.Presentation.Extensions;
using Enrolly.Auth.Authentication;
using Enrolly.Auth.Authentication.Extensions;
using Enrolly.Contracts.Events.Abstractions;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Logging;
using Enrolly.Shared.Logging.Utils.Configurations;
using Enrolly.Shared.Logging.Utils.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.AddObservability();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
builder.Services.Configure<RabbitConfiguration>(builder.Configuration.GetSection("RabbitMQ"));

builder.Services.AddJwtAuthentication();
builder.Services.AddScoped<IAuthorizationHandler, OwnerOrManagerEditAccessHandler>();

builder.Services.AddOpenApi(options => options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());
builder.Services.AddControllers();
builder.Services.AddServices();
builder.Services.AddMessaging();

builder.Services.AddScoped<DomainEventInterceptor>();
builder.Services.AddDbContext<UsersDbContext>((sp, options) =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection"))
        .AddInterceptors(sp.GetRequiredService<DomainEventInterceptor>()));

builder.Services.AddDataProtection();

builder.Services.AddIdentityCore<User>(options =>
    {
        options.User.AllowedUserNameCharacters =
            "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-. ";
    
    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;

    options.ClaimsIdentity.RoleClaimType = "roles"; 
}) 
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<UsersDbContext>()
    .AddDefaultTokenProviders();


var app = builder.Build();

app.UseMiddleware<GlobalExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference((options, httpContext) =>
    {
        var token = httpContext.Request.Cookies["access-token"] ?? string.Empty;
        Console.WriteLine($"[Scalar] access-token cookie: '{token}'");
        
        options
            .AddPreferredSecuritySchemes("Bearer")
            .AddHttpAuthentication("Bearer", auth =>
            {
                auth.Token = token;
            });
        
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.MapGroup("api/v1/auth").MapIdentityApi<User>();

app.MapGroup("api/v1/auth").MapCustomIdentityApiUsingJwt<User>();

//await app.Services.SeedRoles();

app.Run();