using Enrolly.Documents.Application.Abstractions.Services;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Presentation.Attributes;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Endpoints;

[Route("api/v1/users/{applicantId:guid}/education-documents")]
[ApiController]
public class EducationDocumentController : ControllerBase
{
    private readonly IEducationDocumentsMetaService _educationDocumentMetaService;

    public EducationDocumentController(IEducationDocumentsMetaService educationDocumentMetaService)
    {
        _educationDocumentMetaService = educationDocumentMetaService;
    }

    [HttpPost]
    public async Task<IActionResult> PostDocumentInfo(
        [FromRoute] Guid applicantId,
        [FromBody] EducationDocumentMetaDto dto)
    {
        return Ok(_educationDocumentMetaService.CreateDocumentMeta(applicantId, dto));
    }
    
    [RequireOwnerOrManagerEditAccess]
    [HttpGet("{documentId:guid}")]
    public async Task<IActionResult> GetDocumentInfo(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid documentId)
    {
        return Ok(await _educationDocumentMetaService.GetDocumentMeta(documentId));
    }
    
    [RequireOwnerOrManagerEditAccess]
    [HttpPatch("{documentId:guid}")]
    public async Task<IActionResult> ChangeDocumentInfo(
        [FromRoute] Guid applicantId,
        [FromBody] EducationDocumentMetaDto dto)
    {
        return Ok(_educationDocumentMetaService.UpdateDocumentMeta(dto));
    }

    [RequireOwnerOrManagerEditAccess]
    [HttpDelete("{documentId:guid}")]
    public async Task<IActionResult> DeleteDocument(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid documentId)
    {
        return Ok(_educationDocumentMetaService.DeleteDocumentMeta(documentId));
    }
}