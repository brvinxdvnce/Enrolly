using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class ApplicantRepository : IApplicantRepository
{
    private readonly DocumentsDbContext _dbContext;

    public ApplicantRepository(DocumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(Applicant applicant)
    {
        var exists = await _dbContext.Applicants
            .AnyAsync(a => a.Id == applicant.Id);

        return await Result.SuccessIf(!exists, applicant,
                ResultError.Conflict($"Applicant with Id {applicant.Id} already exists"))
            .Tap(async app => await _dbContext.Applicants.AddAsync(app))
            .Tap(async app => await _dbContext.SaveChangesAsync())
            .Bind(app => Result.Success(app.Id));
    }

    public async Task<Result<Applicant>> GetById(Guid applicantId)
    {
        var applicant = await _dbContext.Applicants
            .FirstOrDefaultAsync(a => a.Id == applicantId);

        return Result.SuccessIf(applicant is not null, applicant!,
            ResultError.NotFound($"Applicant with Id {applicantId} not found."));
    }

    public async Task<Result> Update(Applicant applicant)
    {
        return await Result.Try(async () =>
        {
            _dbContext.Applicants.Update(applicant);
            await _dbContext.SaveChangesAsync();
        }, ex => ResultError.Internal(ex.Message));
    }

    public async Task<Result> DeleteById(Guid applicantId)
    {
        var applicant = await _dbContext.Applicants
            .FirstOrDefaultAsync(a => a.Id == applicantId);

        return await Result.SuccessIf(applicant is not null, applicant!,
            ResultError.NotFound($"Applicant with Id {applicantId} not found."))
            .Tap(app => _dbContext.Applicants.Remove(app))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }

    public async Task<Result> IsActive(Guid applicantId)
    {
        var existsActive = await _dbContext.Applicants
            .AnyAsync(a => a.Id == applicantId && a.IsAdmissionActive);
        
        return existsActive
            ? Result.Success()
            : Result.Failure(
                ResultError.NotFound($"Applicant with Id {applicantId} and active admission not found."));
    }
}