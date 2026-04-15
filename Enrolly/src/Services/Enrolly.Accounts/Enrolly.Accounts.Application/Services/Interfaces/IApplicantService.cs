using Enrolly.Accounts.Application.DTOs;

namespace Enrolly.Accounts.Application.Services.Interfaces;

public interface IApplicantService
{
    public Task<Guid> CreateApplicantAsync(Guid id, ApplicantDto dto);
    public Task<ApplicantDto> GetApplicantByIdAsync(Guid id);
    public Task<IEnumerable<ApplicantDto>> GetApplicantsAsync();
    public Task UpdateApplicantAsync(Guid id, ApplicantDto dto);
    public Task DeleteApplicantAsync(Guid id);
}