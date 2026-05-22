using Enrolly.Documents.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Endpoints;

[Route("api/v1/users/{userId:guid}/passport/scans")]
[ApiController]
public class PassportScansController : ControllerBase
{
    private readonly IPassportScansService _passportScansService;

    public PassportScansController(IPassportScansService passportScansService)
    {
        _passportScansService = passportScansService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddPassportScan(IFormFile file, [FromRoute] Guid userId)
    {
        await using var stream = file.OpenReadStream();
        return Ok(await _passportScansService.UploadScan(userId, file.FileName, stream, file.ContentType));
    }

    [HttpGet("{scanId:guid}")]
    public async Task<IActionResult> GetPassportScan(
        [FromRoute] Guid userId,
        [FromRoute] Guid scanId)
    {
        var (stream, contentType, fileName) = await _passportScansService.DownloadScan(userId, scanId);

        return File(stream, contentType, fileName);
    }
    
    [HttpDelete("{scanId:guid}")]
    public async Task<IActionResult> RemovePassportScan(
        [FromRoute] Guid userId,
        [FromRoute] Guid scanId)
    {
        await _passportScansService.DeleteScan(userId, scanId);
        return NoContent();
    }
}
