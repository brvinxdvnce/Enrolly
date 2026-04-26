using Enrolly.Admissions.Application.Abstractions.Services;
using Enrolly.Admissions.Application.DTOs;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Shared.Logging.Utils.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Admissions.Presentation.Endpoints;

public static class AdmissionEndpoints
{
    public static WebApplication AddAdmissionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/admissions");

        group.MapPost("/", CreateAdmission);
        group.MapGet("/", GetAdmissions);
        group.MapGet("/{admissionId:guid}", GetAdmission);
        group.MapPost("/{admissionId:guid}", ChangeAdmissionStatus);
        group.MapDelete("/{admissionId:guid}", DeleteAdmission);
        
        group.MapPost("/{admissionId:guid}/manager", AppointManager);
        group.MapDelete("/{admissionId:guid}/manager", DismissManager);
        
        return app;
    }

    private static async Task<IResult> GetAdmission(
        [FromRoute] Guid admissionId,
        [FromServices] IAdmissionService admissionService)
    {
        var result = await admissionService.GetAdmission(admissionId);
        return result.ToActionResult();
    }

    private static async Task<IResult> CreateAdmission(
        [FromServices] IAdmissionService admissionService,
        [FromBody] AdmissionCreateDto newAdmission)
    {
        var result = await admissionService.CreateAdmission(newAdmission);
        return result.ToActionResult();
    }
    
    public static async Task<IResult> GetAdmissions(
        [FromQuery] string? applicantName,
        [FromQuery] string? program,
        [FromQuery] string? faculty,
        [FromQuery] AdmissionStatus? status,
        [FromQuery] bool? isManaged,
        [FromQuery] Guid? managerId,
        [FromQuery] OrderDirection? lastUpdateSortDirection,
        
        [FromServices] IAdmissionService admissionService,
        
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
    )
    {
        var result = await admissionService.GetAdmissions(applicantName, program, faculty, status, isManaged, managerId, lastUpdateSortDirection, page, pageSize);
        return result.ToActionResult();
    }

    public static async Task<IResult> ChangeAdmissionStatus(
        [FromRoute] Guid admissionId,
        [FromQuery] AdmissionStatus status,
        [FromServices] IAdmissionService admissionService)
    {
        var result = await admissionService.ChangeAdmissionStatus(admissionId, status);
        return result.ToActionResult();
    }

    public static async Task<IResult> DeleteAdmission(
        [FromRoute] Guid admissionId,
        [FromServices] IAdmissionService admissionService)
    {
        var result = await admissionService.DeleteAdmission(admissionId);
        return result.ToActionResult();
    }
    
    public static async Task<IResult> AppointManager(
        [FromQuery] Guid managerId,
        [FromRoute] Guid admissionId,
        [FromServices] IManagerAppointmentService managerAppointmentService)
    {
        var result = await managerAppointmentService.AppointManager(admissionId, managerId);
        return result.ToActionResult();
    }

    public static async Task<IResult> DismissManager(
        [FromRoute] Guid admissionId,
        [FromServices] IManagerAppointmentService managerAppointmentService)
    {
        var result = await managerAppointmentService.DismissManager(admissionId);
        return result.ToActionResult();
    }
}