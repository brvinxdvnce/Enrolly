using Enrolly.Admissions.Application.Abstractions.Services;
using Enrolly.Admissions.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Admissions.Presentation.Endpoints;

[Route("api/v1/admissions")]
public static class AdmissionProgramsEndpoints
{
    public static WebApplication AddAdmissionProgramsEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v1/admissions");
        
        group.MapPost("{admissionId:guid}/programs", AddProgramToAdmission);
        group.MapDelete("{admissionId:guid}/programs/{programId:guid}", RemoveProgramFromAdmission);
        
        group.MapPatch("{admissionId:guid}/programs/{programId:guid}", ChangeProgramPriority);
        
        return app;
    }
    
    public static async Task<IResult> AddProgramToAdmission(
        [FromRoute] Guid id,
        [FromQuery] Guid programId,
        [FromServices] IAdmissionProgramService admissionProgramService,
        [FromQuery] int programPriority = 1
    )
    {
        var result = await admissionProgramService.AddProgramToAdmission(id, programId, programPriority);
        return result.ToActionResult();
    }

    public static async Task<IResult> ChangeProgramPriority(
        [FromRoute] Guid admissionId,
        [FromRoute] Guid programId,
        [FromQuery] int priority,
        [FromServices] IAdmissionProgramService admissionProgramService
    )
    {
        var result = await admissionProgramService.ChangeProgramPriority(admissionId, programId, priority);
        return result.ToActionResult();
    }
    
    public static async Task<IResult> RemoveProgramFromAdmission(
        [FromRoute] Guid admissionId,
        [FromRoute] Guid programId,
        [FromServices] IAdmissionProgramService admissionProgramService)
    {
        var result = await admissionProgramService.RemoveProgramFromAdmission(admissionId, programId);
        return result.ToActionResult();
    }
}