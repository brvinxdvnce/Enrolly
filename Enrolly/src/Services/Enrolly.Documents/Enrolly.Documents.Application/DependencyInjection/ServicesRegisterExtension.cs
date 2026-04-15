using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Documents.Application.DependencyInjection;

public static class ServicesRegisterExtension
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDocumentsService, DocumentMetaService>();
        services.AddScoped<IDocumentScansService, DocumentScansService>();
        services.AddScoped<IPassportService, PassportService>();
        services.AddScoped<IPassportScansService, PassportScansService>();
        
        services.AddScoped<PassportMapper>();
        services.AddScoped<EducationDocumentMapper>();
        
        return services;
    }
}