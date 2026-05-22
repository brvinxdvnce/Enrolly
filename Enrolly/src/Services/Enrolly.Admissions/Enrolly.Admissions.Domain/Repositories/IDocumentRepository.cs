using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IDocumentRepository
{
    public Task<Result> AddAsync(EducationDocument document);
    public Task<Result> DeleteAsync(Guid documentId);
}