using Enrolly.Accounts.Domain.Entities;

namespace Enrolly.Accounts.Domain.Repositories;

public interface IApplicantRepository
{
    public Task<Guid> CreateApplicantAsync(Guid id, Applicant dto);
    public Task<Applicant?> GetApplicantByIdAsync(Guid id);
    public Task<IEnumerable<Applicant?>?> GetApplicantsAsync();
    public Task UpdateApplicantAsync(Guid id, Applicant dto);
    public Task DeleteApplicantAsync(Guid id);
    public Task<bool> RemoveManagerFromApplicantAsync(Guid applicantId, Guid managerId);
    public Task<bool> AddManagerToApplicantAsync(Guid applicantId, Guid managerId);
    public Task<bool> SetAdmissionStatus(Guid applicantId, bool status);
}