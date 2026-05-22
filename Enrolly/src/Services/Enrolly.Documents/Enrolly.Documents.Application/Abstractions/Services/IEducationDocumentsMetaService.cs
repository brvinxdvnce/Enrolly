using Enrolly.Documents.Application.DTOs;

namespace Enrolly.Documents.Application.Abstractions.Services;

public interface IEducationDocumentsMetaService
{
    public Task CreateDocumentMeta(Guid userId, EducationDocumentMetaDto dto);
    public Task<EducationDocumentMetaDto?> GetDocumentMeta(Guid documentId);
    public Task UpdateDocumentMeta(EducationDocumentMetaDto dto);
    public Task DeleteDocumentMeta(Guid id);
}