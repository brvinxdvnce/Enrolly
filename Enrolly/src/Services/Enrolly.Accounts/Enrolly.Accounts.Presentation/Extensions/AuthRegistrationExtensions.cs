using Enrolly.Accounts.Application.Authorization.Requirements;
using Enrolly.Accounts.Infrastructure.Authorization.Handlers;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Enrolly.Accounts.Presentation.Extensions;

public static class AuthRegistrationExtensions
{
    public static IServiceCollection AddAuthPolitics(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.AddRequirements(new OwnerOrManagerEditRequirement());
            });
        });
        
        return services;
    }
    
    public static IServiceCollection AddAuthHandlers(this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, OwnerOrManagerEditAccessHandler>();
        services.AddScoped<IAuthorizationHandler, OwnerOrManagerViewAccessHandler>();
        
        return services;
    }
    
    
}