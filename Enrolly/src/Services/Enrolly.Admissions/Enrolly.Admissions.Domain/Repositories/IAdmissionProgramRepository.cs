using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IAdmissionProgramRepository
{
    public Task<Result> Add(Guid admissionId, Guid programId, int programPriority);
    public Task<Result<AdmissionProgram>> GetById(Guid id);
    public Task<Result> RemoveById(Guid admissionId, Guid programId);
    public Task<Result> ChangeProgramPriority(Guid admissionId, Guid programId, int programPriority);
}