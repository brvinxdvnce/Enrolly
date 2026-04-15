using Enrolly.Accounts.Application.Mappers;
using Enrolly.Accounts.Application.Services.Implementations;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Accounts.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Accounts.Infrastructure.DependencyInjection;

public static class ServiceRegisterExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IManagerService, ManagerService>();
        services.AddScoped<IApplicantService, ApplicantService>();
        services.AddScoped<ICredentialsService, CredentialsService>();
        
        //services.AddScoped<IEmailSender<User>, EmailSender>();
        services.AddScoped<IJwtProvider, JwtProvider>();

        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        services.AddScoped<IManagerRepository, ManagerRepository>();
        
        services.AddScoped<ApplicantMapper>();
        services.AddScoped<ManagerMapper>();
        services.AddScoped<UserMapper>();
        
        return services;
    }
}