using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IApplicantRepository
{
    public Task<Result<Guid>> Add(Applicant applicant);
    public Task<Result<Applicant>> GetById(Guid id);
    public Task<Result> DeleteById(Guid id);
    public Task<Result<Applicant>> Update(Applicant applicant);
    public Task<Result> AddManagerById(Guid applicantId, Guid managerId);
    public Task<Result> DeleteManagerById(Guid applicantId, Guid managerId);
}