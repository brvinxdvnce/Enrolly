using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IFacultyRepository
{
    public Task<Result<Faculty>> Add(Faculty faculty);
    public Task<Result<Faculty>> GetById(Guid id);
    public Task<Result> DeleteById(Guid id);
}