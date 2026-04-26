using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IEducationLevelRepository
{
    public Task<Result<EducationLevel>> Add(EducationLevel educationLevel);
    public Task<Result<EducationLevel>> GetById(Guid id);
    public Task<Result> DeleteById(Guid id);
}