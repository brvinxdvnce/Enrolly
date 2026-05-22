using System.Security.Claims;
using Enrolly.Admissions.Application.Abstractions.Services;
using Enrolly.Admissions.Application.DTOs;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Admissions.Presentation.Extensions;
using Enrolly.Admissions.Presentation.ResultUtils;
using Enrolly.Shared.Logging.Utils.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Admissions.Presentation.Endpoints;

public static class AdmissionEndpoints
{
    public static WebApplication AddAdmissionEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/applicants/").RequireAuthorization();

        group.MapGet("/admissions", GetAdmissions)
            .WithDisplayName("Просмотреть все заявки в системе.");
        
        group.MapGet("{applicantId:guid}/admissions", GetAdmissionsByApplicantId)
            .RequireAdmissionEditAccess()
            .WithDisplayName("Посмотреть все заявки конкретного абитуриента.");
        
        group.MapPost("{applicantId:guid}/admissions", CreateAdmission)
            .WithDisplayName("Создать заявку у абитуриента.");
        
        group.MapGet("{applicantId:guid}/admissions/{admissionId:guid}", GetAdmission)
            .RequireAdmissionEditAccess()
            .WithDisplayName("Просмотреть заявку абитуриента.");
        
        group.MapPost("{applicantId:guid}/admissions/{admissionId:guid}", ChangeAdmissionStatus)
            .RequireAdmissionEditAccess()
            .WithDisplayName("Изменить статус заявки абитуриента.");
        
        group.MapDelete("{applicantId:guid}/admissions/{admissionId:guid}", DeleteAdmission)
            .RequireAdmissionEditAccess()
            .WithDisplayName("Удалить заявку абитуриента.");
        
        group.MapPost("{applicantId:guid}/admissions/{admissionId:guid}/manager", AppointManager)
            .WithDisplayName("Назначить менеджера ответственным за заявку.");
        
        group.MapDelete("{applicantId:guid}/admissions/{admissionId:guid}/manager", DismissManager)
            .WithDisplayName("Снять с менеджера ответственность за заявку.");
        
        return app;
    }

    private static async Task<IResult> GetAdmission(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid admissionId,
        [FromServices] IAdmissionService admissionService,
        [FromServices] IAuthorizationService authorizationService,
        ClaimsPrincipal userClaims)
    {
        var result = await admissionService.GetAdmission(admissionId);
        return result.ToActionResult();
    }

    private static async Task<IResult> GetAdmissionsByApplicantId(
        [FromRoute] Guid applicantId,
        [FromServices] IAdmissionService admissionService,
        [FromServices] IAuthorizationService authorizationService,
        ClaimsPrincipal userClaims)
    {
        var result = await admissionService.GetAdmissionsByApplicantId(applicantId);
        return result.ToActionResult();
    }

    private static async Task<IResult> CreateAdmission(
        [FromRoute] Guid applicantId,
        [FromServices] IAdmissionService admissionService)
    {
        var result = await admissionService.CreateAdmission(applicantId);
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
        [FromRoute] Guid applicantId,
        [FromRoute] Guid admissionId,
        [FromQuery] AdmissionStatus status,
        [FromServices] IAdmissionService admissionService)
    {
        var result = await admissionService.ChangeAdmissionStatus(admissionId, status);
        return result.ToActionResult();
    }

    public static async Task<IResult> DeleteAdmission(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid admissionId,
        [FromServices] IAdmissionService admissionService)
    {
        var result = await admissionService.DeleteAdmission(admissionId);
        return result.ToActionResult();
    }
    
    public static async Task<IResult> AppointManager(
        [FromRoute] Guid applicantId,
        [FromQuery] Guid managerId,
        [FromRoute] Guid admissionId,
        [FromServices] IManagerAppointmentService managerAppointmentService)
    {
        var result = await managerAppointmentService.AppointManager(admissionId, managerId);
        return result.ToActionResult();
    }

    public static async Task<IResult> DismissManager(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid admissionId,
        [FromServices] IManagerAppointmentService managerAppointmentService)
    {
        var result = await managerAppointmentService.DismissManager(admissionId);
        return result.ToActionResult();
    }
}