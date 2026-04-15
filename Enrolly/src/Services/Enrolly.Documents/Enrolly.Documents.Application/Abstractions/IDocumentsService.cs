using Enrolly.Documents.Application.DTOs;

namespace Enrolly.Documents.Application.Abstractions;

public interface IDocumentsService
{
    public Task CreateDocumentMeta(Guid userId, DiplomaMetaDto dto);
    public Task<DiplomaMetaDto?> GetDocumentMeta(Guid documentId);
    public Task UpdateDocumentMeta(DiplomaMetaDto dto);
    public Task DeleteDocumentMeta(Guid id);
}