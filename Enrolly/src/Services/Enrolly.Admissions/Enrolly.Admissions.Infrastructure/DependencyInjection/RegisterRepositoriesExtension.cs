using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Admissions.Infrastructure.DependencyInjection;

public static class RegisterRepositoriesExtension
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAdmissionProgramRepository, AdmissionProgramRepository>();
        services.AddScoped<IAdmissionRepository,AdmissionRepository>();
        services.AddScoped<IApplicantRepository,ApplicantRepository>();
        services.AddScoped<IDocumentRepository,DocumentRepository>();
        services.AddScoped<IEducationLevelRepository,EducationLevelRepository>();
        services.AddScoped<IFacultyRepository,FacultyRepository>();
        services.AddScoped<IManagerRepository,ManagerRepository>();
        services.AddScoped<IProgramRepository,ProgramRepository>();

        return services;
    }
}