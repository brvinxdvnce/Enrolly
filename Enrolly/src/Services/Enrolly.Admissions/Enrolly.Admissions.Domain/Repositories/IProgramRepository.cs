using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IProgramRepository
{
    public Task<Result<Guid>> Add(Program program);
    public Task<Result<Program>> GetById(Guid id);
    public Task<Result> DeleteById(Guid id);
}