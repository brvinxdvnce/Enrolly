using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Controllers;

[Route("api/v1/users/{userId:guid}/education-documents")]
[ApiController]
public class EducationDocumentController : ControllerBase
{
    private readonly IDocumentsService _documentService;

    public EducationDocumentController(IDocumentsService documentService)
    {
        _documentService = documentService;
    }

    [HttpPost]
    public async Task<IActionResult> PostDocumentInfo(
        [FromRoute] Guid userId,
        [FromBody] DiplomaMetaDto dto)
    {
        return Ok(_documentService.CreateDocumentMeta(userId, dto));
    }
    
    [HttpGet("{docId:guid}")]
    public async Task<IActionResult> GetDocumentInfo(
        [FromRoute] Guid userId,
        [FromRoute] Guid docId)
    {
        return Ok(_documentService.GetDocumentMeta(docId));
    }
    
    [HttpPatch("{docId:guid}")]
    public async Task<IActionResult> ChangeDocumentInfo(
        [FromRoute] Guid userId,
        [FromBody] DiplomaMetaDto dto)
    {
        return Ok(_documentService.UpdateDocumentMeta(dto));
    }

    [HttpDelete("{docId:guid}")]
    public async Task<IActionResult> DeleteDocument(
        [FromRoute] Guid userId,
        [FromRoute] Guid docId)
    {
        return Ok(_documentService.DeleteDocumentMeta(docId));
    }
}