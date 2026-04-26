using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class ProgramRepository : IProgramRepository
{
    public Task<Result<Guid>> Add(Program program)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Program>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteById(Guid id)
    {
        throw new NotImplementedException();
    }
}