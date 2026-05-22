using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Accounts.Infrastructure.Database;
using Enrolly.Contracts.Events;
using Enrolly.Contracts.Events.Events;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Accounts.Infrastructure.Repositories;

public class ApplicantRepository : IApplicantRepository
{
    private readonly UsersDbContext _dbContext;

    public ApplicantRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateApplicantAsync(Guid id, Applicant dto)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new Exception();
        dto.AddEvent(new ApplicantRegisteredEvent(dto.Id, user.UserName, user.Email));
        
        user.ApplicantProfile = dto;
        await _dbContext.SaveChangesAsync();
        return dto.Id;
    }

    public Task<Applicant?> GetApplicantByIdAsync(Guid id)
    {
        return _dbContext.Applicants
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Applicant?>?> GetApplicantsAsync()
    {
        return await _dbContext.Applicants.ToListAsync();
    }

    public async Task UpdateApplicantAsync(Guid id, Applicant dto)
    {
        var applicant = _dbContext.Applicants.FirstOrDefault(x => x.Id == id);
        _dbContext.Entry(applicant).CurrentValues
            .SetValues(new { dto.DateOfBirth, dto.CitizenshipId, dto.Gender, dto.Citizenship });
    
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteApplicantAsync(Guid id)
    {
        var applicant = await _dbContext.Applicants.FirstOrDefaultAsync(a => a.Id == id);
        applicant.AddEvent(new ApplicantDeletedEvent(applicant.Id));
        _dbContext.Applicants.Remove(applicant);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task<bool> RemoveManagerFromApplicantAsync(Guid applicantId, Guid managerId)
    {
        var applicant = await _dbContext.Applicants
            .Include(a => a.Managers)
            .FirstOrDefaultAsync(a => a.Id == applicantId);

        if (applicant is null)
            return false;

        var managerToRemove = applicant.Managers.FirstOrDefault(m => m.Id == managerId);
        if (managerToRemove == null)
            return false;

        applicant.Managers.Remove(managerToRemove);

        await _dbContext.SaveChangesAsync();
        
        return true;
    }

    public async Task<bool> AddManagerToApplicantAsync(Guid applicantId, Guid managerId)
    {
        var applicant = await _dbContext.Applicants
            .Include(a => a.Managers)
            .FirstOrDefaultAsync(a => a.Id == applicantId);
        if (applicant is null) return false;
        
        var manager = await _dbContext.Managers
            .FirstOrDefaultAsync(m => m.Id == managerId);
        if (manager is null) return false;

        if (!applicant.Managers.Contains(manager))
        {
            applicant.Managers.Add(manager);
            await _dbContext.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> SetAdmissionStatus(Guid applicantId, bool status)
    {
        var applicant = await _dbContext.Applicants
            .FirstOrDefaultAsync(a => a.Id == applicantId);
        if (applicant is null) return false;
        
        if (applicant.IsActiveAdmission == status) return true;

        applicant.IsActiveAdmission = status;
        await _dbContext.SaveChangesAsync();
        return true;
    }
}