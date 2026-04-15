using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Accounts.Infrastructure.Database;
using Enrolly.Contracts.Events;
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
}