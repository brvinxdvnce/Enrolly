using Enrolly.EduDictionary.Application.Mappings;
using Enrolly.EduDictionary.Application.Repositories;
using Enrolly.EduDictionary.Application.Services.Implementations;
using Enrolly.EduDictionary.Application.Services.Interfaces;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.EduDictionary.Application.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddScoped<IEducationLevelRepository, EducationLevelRepository>();
        services.AddScoped<IImportSummaryRepository, ImportSummaryRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<IProgramRepository, ProgramRepository>();
        
        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddScoped<DocumentTypeMapper>();
        services.AddScoped<EducationLevelMapper>();
        services.AddScoped<FacultyMapper>();
        services.AddScoped<ProgramMapper>();
        
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IExternalDataCollector, ExternalDataCollector>();
        
        return services;
    }
    
    /*public static IServiceCollection ConfigureHttpClient(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddHttpClient("1c-mockup.kreosoft.client", client =>
        {
            client.BaseAddress = new Uri("https://1c-mockup.kreosoft.space");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                Convert.ToBase64String(
                    Encoding.ASCII.GetBytes(
                        builder.Configuration.GetConnectionString("KreosoftConnection")
                        ?? string.Empty)));
        });
            
        return services;
    }*/
}