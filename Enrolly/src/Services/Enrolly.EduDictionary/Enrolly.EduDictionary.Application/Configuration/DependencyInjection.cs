using System.Net.Http.Headers;
using System.Text;
using Enrolly.EduDictionary.Application.Mappings;
using Enrolly.EduDictionary.Application.Repositories;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.EduDictionary.Application.Configuration;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IDocumentTypeRepository, DocumentTypeRepository>();
        services.AddSingleton<IEducationLevelRepository, EducationLevelRepository>();
        services.AddSingleton<IFacultyRepository, FacultyRepository>();
        services.AddSingleton<IProgramRepository, ProgramRepository>();
        return services;
    }

    public static IServiceCollection AddMappers(this IServiceCollection services)
    {
        services.AddSingleton<DocumentTypeMapper>();
        services.AddSingleton<EducationLevelMapper>();
        services.AddSingleton<FacultyMapper>();
        services.AddSingleton<ProgramMapper>();
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