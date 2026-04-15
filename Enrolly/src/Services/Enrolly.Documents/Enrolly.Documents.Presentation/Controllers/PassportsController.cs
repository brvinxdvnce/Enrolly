using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Controllers;

[Route("api/v1/users/{userId:guid}/passport")]
[ApiController]
public class PassportsController : ControllerBase
{
    private readonly IPassportService _passportService;

    public PassportsController(IPassportService passportService)
    {
        _passportService = passportService;
    }

    [HttpPost]
    public async Task<IActionResult> PostPassportInfo(
        [FromRoute] Guid userId,
        [FromBody] PassportMetaDto dto)
    {
        return Ok(await _passportService.CreatePassportMeta(userId, dto));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPassportInfo(
        [FromRoute] Guid userId)
    {
        return Ok(await _passportService.GetPassportMeta(userId));
    }
    
    [HttpPatch]
    public async Task<IActionResult> ChangePassportInfo(
        [FromRoute] Guid userId,
        [FromRoute] PassportMetaDto dto)
    {
        await _passportService.UpdatePassportMeta(userId, dto);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePassport(
        [FromRoute] Guid userId)
    {
        await _passportService.DeletePassportMeta(userId);
        return NoContent();
    }
}