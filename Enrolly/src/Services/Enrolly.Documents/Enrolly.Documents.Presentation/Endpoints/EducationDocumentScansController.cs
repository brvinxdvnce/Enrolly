using Enrolly.Documents.Application.Abstractions.Services;
using Enrolly.Documents.Presentation.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Endpoints;

[Route("api/v1/users/{userId:guid}/education-documents/{documentId:guid}/scans")]
[ApiController]
public class EducationDocumentScansController : ControllerBase
{
    private readonly IEducationDocumentScansService _educationDocumentScansService;
    
    public EducationDocumentScansController(IEducationDocumentScansService educationDocumentScansService)
    {
        _educationDocumentScansService = educationDocumentScansService;
    }
    
    [RequireOwnerOrManagerEditAccess]
    [HttpPost]
    public async Task<IActionResult> AddDocumentScan(
        IFormFile file,
        [FromRoute] Guid documentId,
        [FromRoute] Guid userId)
    {
        await using var stream = file.OpenReadStream();
        return Ok(await _educationDocumentScansService.UploadScan(userId, documentId, file.FileName, stream, file.ContentType));
    }
    
    [RequireOwnerOrManagerEditAccess]
    [HttpGet("{scanId:guid}")]
    public async Task<IActionResult> GetDocumentScan(
        [FromRoute] Guid scanId,
        [FromRoute] Guid documentId,
        [FromRoute] Guid userId)
    {
        var (stream, contentType, fileName) = await _educationDocumentScansService.DownloadScan(userId, documentId, scanId);

        return new FileStreamResult(stream, contentType) { FileDownloadName = fileName };
    }
    
    [RequireOwnerOrManagerEditAccess]
    [HttpDelete("{scanId:guid}")]
    public async Task<IActionResult> RemoveDocumentScan(
        [FromRoute] Guid scanId,
        [FromRoute] Guid documentId,
        [FromRoute] Guid userId)
    {
        await _educationDocumentScansService.DeleteScan(userId, documentId, scanId);
        return NoContent();
    }
}