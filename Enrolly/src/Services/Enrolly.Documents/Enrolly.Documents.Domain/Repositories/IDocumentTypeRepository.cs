using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;

namespace Enrolly.Documents.Domain.Repositories;

public interface IDocumentTypeRepository
{
    public Task<Result> Add(EducationDocumentType documentType);
    public Task<Result> Update(EducationDocumentType documentType);
    public Task<Result<EducationDocumentType>> GetById(Guid documentTypeId);
    public Task<Result> DeleteById(Guid documentTypeId);
}