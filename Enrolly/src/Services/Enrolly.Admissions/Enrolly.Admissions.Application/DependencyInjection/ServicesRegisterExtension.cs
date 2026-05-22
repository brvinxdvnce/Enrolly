using Enrolly.Admissions.Application.Abstractions.Services;
using Enrolly.Admissions.Application.Mappers;
using Enrolly.Admissions.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Enrolly.Admissions.Application.DependencyInjection;

public static class ServicesRegisterExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IManagerAppointmentService, ManagerAppointmentService>();
        services.AddScoped<IAdmissionProgramService, AdmissionProgramService>();
        services.AddScoped<IAdmissionService, AdmissionService>();

        services.AddScoped<AdmissionMapper>();
        
        return services;
    }
}