using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;

namespace Enrolly.Documents.Domain.Repositories;

public interface IEducationDocumentRepositoryV2
{
    public Task<Result<IReadOnlyCollection<EducationDocument>>> GetAllByUserIdAsync(Guid userId);
    public Task<Result<EducationDocument>> GetByIdAsync(Guid documentId);
    public Task<Result<Guid>> CreateAsync(EducationDocument educationDocument);
    public Task<Result> UpdateAsync(EducationDocument educationDocument);
    public Task<Result> DeleteAsync(Guid documentId);
}