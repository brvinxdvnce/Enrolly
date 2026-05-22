using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IEducationDocumentTypeRepository
{
    public Task<Result> Add(EducationDocumentType faculty);
    public Task<Result> Update(EducationDocumentType educationLevel);
    public Task<Result<EducationDocumentType>> GetById(Guid id);
    public Task<Result> DeleteById(Guid id);
}