using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Presentation.EndpointAttributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Authorize]
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
    
    [HttpPost("{applicantId:guid}")]
    public async Task<IActionResult> CreateApplicantProfile(
        [FromRoute] Guid applicantId,
        [FromBody] ApplicantDto dto)
    {
        return Ok(await _applicantService.CreateApplicantAsync(applicantId, dto));
    }
    
    [OwnerOrManagerEditAccess]
    [HttpGet("{applicantId:guid}")]
    public async Task<IActionResult> GetApplicantInfo(
        [FromRoute] Guid applicantId)
    {
        return Ok(await _applicantService.GetApplicantByIdAsync(applicantId));
    }
    
    [OwnerOrManagerEditAccess]
    [HttpPatch("{applicantId:guid}")]
    public async Task<IActionResult> UpdateApplicantInfo(
        [FromRoute] Guid applicantId,
        [FromBody] ApplicantDto dto)
    {
        await _applicantService.UpdateApplicantAsync(applicantId, dto);
        return NoContent();
    }
    
    [OwnerOrManagerEditAccess]
    [HttpDelete("{applicantId:guid}")]
    public async Task<IActionResult> DeleteApplicantProfile(
        [FromRoute] Guid applicantId)
    {
        await _applicantService.DeleteApplicantAsync(applicantId);
        return NoContent();
    }
}