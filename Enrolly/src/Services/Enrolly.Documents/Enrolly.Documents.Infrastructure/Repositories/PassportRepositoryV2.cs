using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class PassportRepositoryV2 : IPassportRepositoryV2
{
    private readonly DocumentsDbContext _dbContext;

    public PassportRepositoryV2(DocumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Passport>> GetByIdAsync(Guid applicantId)
    {
        var passport = await _dbContext.Passports
            .FirstOrDefaultAsync(p => p.Id == applicantId);

        return Result.SuccessIf(passport is not null, passport!,
            ResultError.NotFound($"Passport with Id {applicantId} not found."));
    }

    public async Task<Result<Guid>> CreateAsync(Passport passport)
    {
        var exists = await _dbContext.Passports
            .AnyAsync(p => p.Id == passport.Id);

        return await Result.SuccessIf(!exists, passport,
                ResultError.Conflict("Passport already exists"))
            .Tap(p => _dbContext.Passports.Add(passport))
            .Tap(async p => await _dbContext.SaveChangesAsync())
            .Bind(p => Result.Success(p.Id));
    }

    public async Task<Result> UpdateAsync(Passport passport)
    {
        return await Result.Try(async () =>
        {
            _dbContext.Passports.Update(passport);
            await _dbContext.SaveChangesAsync();
        }, ex => ResultError.Internal(ex.Message));
    }

    public async Task<Result> DeleteAsync(Guid applicantId)
    {
        var passport = await _dbContext.Passports
            .FirstOrDefaultAsync(p => p.Id == applicantId);

        return await Result.SuccessIf(passport is not null, passport!,
            ResultError.NotFound())
            .Tap(p => _dbContext.Remove(p))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }
}