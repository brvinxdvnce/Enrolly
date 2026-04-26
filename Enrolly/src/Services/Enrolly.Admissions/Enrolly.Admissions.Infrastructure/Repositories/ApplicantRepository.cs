
using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class ApplicantRepository : IApplicantRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public ApplicantRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(Applicant applicant)
    {
        _dbContext.Applicants.Add(applicant);
        await _dbContext.SaveChangesAsync();
        return Result.Success(applicant.Id);
    }

    public async Task<Result<Applicant>> GetById(Guid id)
    {
        var applicant = await _dbContext.Applicants
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return Result.SuccessIf(
            applicant is not null,
            applicant!,
            ResultError.NotFound($"Applicant with id {id} not found."));
    }

    public async Task<Result> DeleteById(Guid id)
    {
        var applicant = await _dbContext.Applicants.FirstOrDefaultAsync(a => a.Id == id);   
        
        return await Result.SuccessIf(
                applicant is not null, 
                ResultError.NotFound($"Applicant with id {id} not found."))
            .Tap(() => _dbContext.Applicants.Remove(applicant!))
            .Tap(async () => await _dbContext.SaveChangesAsync());
    }

    public async Task<Result> AddManagerById(Guid applicantId, Guid managerId)
    {
        var applicant = await _dbContext.Applicants.FirstOrDefaultAsync(a => a.Id == applicantId);
        
        return await Result.SuccessIf(
                applicant is not null, 
                ResultError.NotFound($"Applicant with id {applicantId} not found."))
            .Tap(() => applicant!.Managers.Add(managerId))
            .Tap(async () => await _dbContext.SaveChangesAsync());
    }

    public async Task<Result> DeleteManagerById(Guid applicantId, Guid managerId)
    {
        var applicant = await _dbContext.Applicants.FirstOrDefaultAsync(a => a.Id == applicantId);
        
        return await Result.SuccessIf(applicant is not null, applicant!, ResultError.NotFound("Applicant not found"))
            .Ensure(ap => ap.Managers.Contains(managerId),
                ResultError.NotFound("Manager not found"))
            .Tap(ap => ap.Managers.Remove(ap.Managers.First(id => id == managerId)))
            .Tap(async _ => await _dbContext.SaveChangesAsync());
    }
}