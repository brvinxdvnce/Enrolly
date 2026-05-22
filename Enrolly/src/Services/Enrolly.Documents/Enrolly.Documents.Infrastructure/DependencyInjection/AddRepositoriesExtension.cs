using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Documents.Infrastructure.DependencyInjection;

public static class AddRepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IApplicantRepository, ApplicantRepository>();
        
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        
        services.AddScoped<IEducationDocumentRepository, EducationDocumentRepository>();
        services.AddScoped<IEducationDocumentRepositoryV2, EducationDocumentRepositoryV2>();
        
        services.AddScoped<IManagerRepository, ManagerRepository>();
        
        services.AddScoped<IPassportRepository, PassportRepository>();
        services.AddScoped<IPassportRepositoryV2, PassportRepositoryV2>();
        
        services.AddScoped<IScanRepository, ScanRepository>();
        
        return services;
    }
}