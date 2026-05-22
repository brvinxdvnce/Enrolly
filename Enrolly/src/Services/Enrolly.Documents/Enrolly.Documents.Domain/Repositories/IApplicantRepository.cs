using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;

namespace Enrolly.Documents.Domain.Repositories;

public interface IApplicantRepository
{
    public Task<Result<Guid>> Add(Applicant applicant);
    public Task<Result<Applicant>> GetById(Guid applicantId);
    public Task<Result> Update(Applicant applicant);
    public Task<Result> DeleteById(Guid applicantId);
    public Task<Result> IsActive(Guid applicantId);
}