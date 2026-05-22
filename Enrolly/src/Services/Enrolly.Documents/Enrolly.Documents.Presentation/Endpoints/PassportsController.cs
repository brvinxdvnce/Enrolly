using Enrolly.Documents.Application.Abstractions.Services;
using Enrolly.Documents.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Endpoints;

[Route("api/v1/users/{userId:guid}/passport")]
[ApiController]
public class PassportsController : ControllerBase
{
    private readonly IPassportMetaService _passportMetaService;

    public PassportsController(IPassportMetaService passportMetaService)
    {
        _passportMetaService = passportMetaService;
    }

    [HttpPost]
    public async Task<IActionResult> PostPassportInfo(
        [FromRoute] Guid userId,
        [FromBody] PassportMetaDto dto)
    {
        return Ok(await _passportMetaService.CreatePassportMeta(userId, dto));
    }
    
    [HttpGet]
    public async Task<IActionResult> GetPassportInfo(
        [FromRoute] Guid userId)
    {
        return Ok(await _passportMetaService.GetPassportMeta(userId));
    }
    
    [HttpPatch]
    public async Task<IActionResult> ChangePassportInfo(
        [FromRoute] Guid userId,
        [FromBody] UpdatePassportRequestDto dto)
    {
        await _passportMetaService.UpdatePassportMeta(userId, dto);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeletePassport(
        [FromRoute] Guid userId)
    {
        await _passportMetaService.DeletePassportMeta(userId);
        return NoContent();
    }
}