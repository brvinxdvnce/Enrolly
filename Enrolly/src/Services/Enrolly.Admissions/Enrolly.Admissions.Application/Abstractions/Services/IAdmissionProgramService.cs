using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Application.Abstractions.Services;

public interface IAdmissionProgramService
{
    public Task<Result> GetAdmissionPrograms(Guid admissionId);
    public Task<Result> AddProgramToAdmission(Guid admissionId, Guid programId, int programPriority = 1);
    public Task<Result> RemoveProgramFromAdmission(Guid admissionId, Guid programId);
    public Task<Result> ChangeProgramPriority(Guid admissionId, Guid programId, int newPriority);
}