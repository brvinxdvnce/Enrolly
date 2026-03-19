using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Admissions.Presentation.Controllers;

[ApiController]
[Route("api/v1/admissions")]
public class AdmissionsProgramsController : ControllerBase
{
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAdmissionPrograms()
    {
        return Ok();
    }
    
    [HttpPost("{id:guid}/programs")]
    public async Task<IActionResult> AddProgramToAdmission(
        [FromRoute] Guid id,
        [FromBody] Guid? programId
    )
    {
        return Ok();
    }

    [HttpPatch("{id:guid}/programs/{programId:guid}")]
    public async Task<IActionResult> ChangeProgramPriority(
        [FromRoute] Guid id,
        [FromBody] int priority
    )
    {
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveProgramFromAdmission()
    {
        return Ok();
    }
}