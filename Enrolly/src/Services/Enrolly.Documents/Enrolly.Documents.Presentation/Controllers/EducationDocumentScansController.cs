using Enrolly.Documents.Application.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Controllers;

[Route("api/v1/users/{userId:guid}/education-documents/{docId:guid}/scans")]
[ApiController]
public class EducationDocumentScansController : ControllerBase
{
    private readonly IDocumentScansService _documentScansService;
    
    public EducationDocumentScansController(IDocumentScansService documentScansService)
    {
        _documentScansService = documentScansService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddDocumentScan(
        IFormFile file,
        [FromRoute] Guid docId,
        [FromRoute] Guid userId)
    {
        await using var stream = file.OpenReadStream();
        return Ok(await _documentScansService.UploadScan(userId, docId, file.FileName, stream, file.ContentType));
    }
    
    [HttpGet("{scanId:guid}")]
    public async Task<IActionResult> GetDocumentScan(
        [FromRoute] Guid scanId,
        [FromRoute] Guid docId,
        [FromRoute] Guid userId)
    {
        var (stream, contentType, fileName) = await _documentScansService.DownloadScan(userId, docId, scanId);

        return new FileStreamResult(stream, contentType) { FileDownloadName = fileName };
    }
    
    [HttpDelete("{scanId:guid}")]
    public async Task<IActionResult> RemoveDocumentScan(
        [FromRoute] Guid scanId,
        [FromRoute] Guid docId,
        [FromRoute] Guid userId)
    {
        await _documentScansService.DeleteScan(userId, docId, scanId);
        return NoContent();
    }
}