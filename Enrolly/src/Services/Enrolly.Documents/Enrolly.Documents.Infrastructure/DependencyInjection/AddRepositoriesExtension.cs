using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Documents.Infrastructure.DependencyInjection;

public static class AddRepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDiplomaRepository, DiplomaRepository>();
        services.AddScoped<IPassportRepository, PassportRepository>();
        services.AddScoped<IScanRepository, ScanRepository>();
        
        return services;
    }
}