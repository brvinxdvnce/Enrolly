using Enrolly.Documents.Application.Abstractions.ServicesV2;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Documents.Presentation.Endpoints;

public static class EducationDocumentEndpoints
{
    public static WebApplication AddEducationDocumentEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("api/v2/users/{applicantId:guid}/education-documents");

        group.MapPost("/", PostDocumentInfo);
        group.MapGet("/", GetAllDocuments);
        group.MapGet("/{documentId:guid}", GetDocumentInfo);
        group.MapPatch("/{documentId:guid}", ChangeDocumentInfo);
        group.MapDelete("/{documentId:guid}", DeleteDocument);
        
        return app;
    }

    private static async Task<IResult> PostDocumentInfo(
        [FromRoute] Guid applicantId,
        [FromBody] EducationDocumentMetaCreateDto documentMetaCreateDto,
        [FromServices] IEducationDocumentsMetaServiceV2 educationDocumentsMetaService)
    {
        var result = await educationDocumentsMetaService.CreateDocumentMeta(applicantId, documentMetaCreateDto);
        return result.ToActionResult();
    }
    
    private static async Task<IResult> GetAllDocuments(
        [FromRoute] Guid applicantId,
        [FromServices] IEducationDocumentsMetaServiceV2 educationDocumentsMetaService)
    {
        var result = await educationDocumentsMetaService.GetAllDocumentsByApplicantId(applicantId);
        return result.ToActionResult();
    }
    
    private static async Task<IResult> GetDocumentInfo(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid documentId,
        [FromServices] IEducationDocumentsMetaServiceV2 educationDocumentsMetaService)
    {
        var result = await educationDocumentsMetaService.GetDocumentMeta(documentId);
        return result.ToActionResult();
    }
    
    private static async Task<IResult> ChangeDocumentInfo(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid documentId,
        [FromBody] EducationDocumentMetaDto documentMetaCreateDto,
        [FromServices] IEducationDocumentsMetaServiceV2 educationDocumentsMetaService)
    {
        var result = await educationDocumentsMetaService.UpdateDocumentMeta(applicantId, documentMetaCreateDto);
        return result.ToActionResult();
    }
    
    private static async Task<IResult> DeleteDocument(
        [FromRoute] Guid applicantId,
        [FromRoute] Guid documentId,
        [FromServices] IEducationDocumentsMetaServiceV2 educationDocumentsMetaService)
    {
        var result = await educationDocumentsMetaService.DeleteDocumentMeta(applicantId, documentId);
        return result.ToActionResult();
    }
}