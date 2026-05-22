using CSharpFunctionalExtensions;
using Enrolly.Documents.Application.DTOs;

namespace Enrolly.Documents.Application.Abstractions.ServicesV2;

public interface IEducationDocumentsMetaServiceV2
{
    public Task<Result<Guid>> CreateDocumentMeta(Guid applicantId, EducationDocumentMetaCreateDto dto);
    public Task<Result<IReadOnlyCollection<EducationDocumentMetaDto>>> GetAllDocumentsByApplicantId(Guid applicantId);
    public Task<Result<EducationDocumentMetaDto>> GetDocumentMeta(Guid documentId);
    public Task<Result> UpdateDocumentMeta(Guid applicantId, EducationDocumentMetaDto dto);
    public Task<Result> DeleteDocumentMeta(Guid applicantId, Guid documentId);
}