using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.Abstractions.Services;
using Enrolly.Documents.Application.Abstractions.ServicesV2;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Application.Services;
using Enrolly.Documents.Application.ServicesV2;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Documents.Application.DependencyInjection;

public static class ServicesRegisterExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IEducationDocumentsMetaService, EducationDocumentMetaService>();
        services.AddScoped<IEducationDocumentsMetaServiceV2, EducationDocumentsMetaServiceV2>();
        
        services.AddScoped<IEducationDocumentScansService, EducationDocumentScansService>();
        
        services.AddScoped<IPassportMetaService, PassportMetaService>();
        services.AddScoped<IPassportMetaServiceV2, PassportMetaServiceV2>();
        
        services.AddScoped<IPassportScansService, PassportScansService>();
        
        services.AddScoped<PassportMapper>();
        services.AddScoped<EducationDocumentMapper>();
        
        return services;
    }
}