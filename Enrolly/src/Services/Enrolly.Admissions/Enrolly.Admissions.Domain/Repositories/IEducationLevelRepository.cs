using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IEducationLevelRepository
{
    public Task<Result> Add(EducationLevel educationLevel);
    public Task<Result> Update(EducationLevel educationLevel);
    public Task<Result<EducationLevel>> GetById(int id);
    public Task<Result> DeleteById(int id);
}