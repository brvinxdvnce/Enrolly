using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Route("api/v1/applicants")]
[ApiController]
public class ApplicantsController : ControllerBase
{
    private readonly ILogger<ApplicantsController> _logger;
    private readonly IApplicantService _applicantService;
    
    public ApplicantsController(IApplicantService applicantService, ILogger<ApplicantsController> logger)
    {
        _applicantService = applicantService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetApplicant()
    {
        return Ok(await _applicantService.GetApplicantsAsync());
    }
    
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> CreateApplicantProfile(
        [FromRoute] Guid id,
        [FromBody] ApplicantDto dto)
    {
        return Ok(await _applicantService.CreateApplicantAsync(id, dto));
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetApplicantInfo(
        [FromRoute] Guid id)
    {
        return Ok(await _applicantService.GetApplicantByIdAsync(id));
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateApplicantInfo(
        [FromRoute] Guid id,
        [FromBody] ApplicantDto dto)
    {
        await _applicantService.UpdateApplicantAsync(id, dto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteApplicantProfile(
        [FromRoute] Guid id)
    {
        await _applicantService.DeleteApplicantAsync(id);
        return NoContent();
    }
}