using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Admissions.Presentation.Controllers;

[Route("api/v1/admissions")]
[ApiController]
public class AdmissionsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateAdmission()
    {
        return Ok();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAdmissions(
        [FromQuery] string? name,
        [FromQuery] string? program,
        [FromQuery] string? faculty,
        [FromQuery] AdmissionStatus? status,
        [FromQuery] bool isManaged,
        [FromQuery] Guid managerId,
        [FromQuery] string dateSort,
        
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
        )
    {
        return Ok();
    }

    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> ChangeAdmissionStatus(
        [FromQuery] AdmissionStatus? status)
    {
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAdmission(Guid id)
    {
        return Ok();
    }
    
    
    [HttpPost("{id:guid}/manager")]
    public async Task<IActionResult> AppointManager(
        [FromRoute] Guid id)
    {
        return Ok();
    }

    [HttpDelete("{id:guid}/manager")]
    public async Task<IActionResult> DismissManager(
        [FromRoute] Guid id)
    {
        return Ok();
    }
}