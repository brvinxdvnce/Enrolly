using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IDocumentRepository
{
    public Task<Result> AddAsync(Document document);
    public Task<Result> DeleteAsync(Guid documentId);
}