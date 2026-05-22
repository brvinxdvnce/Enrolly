using Enrolly.Auth.Authentication.Politics;
using Enrolly.Documents.Application.Authorization.Requirements;
using Enrolly.Documents.Infrastructure.Authorization.Handlers;
using Microsoft.AspNetCore.Authorization;

namespace Enrolly.Documents.Presentation.Extensions;

public static class AuthRegistrationExtensions
{
    public static IServiceCollection AddAuthPotitics (this IServiceCollection services)
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
    
    public static IServiceCollection AddAuthHandlers (this IServiceCollection services)
    {
        services.AddScoped<IAuthorizationHandler, OwnerOrManagerAdmissionEditHandler>();
        
        return services;
    }
    
    
}