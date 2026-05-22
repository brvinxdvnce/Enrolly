using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class EducationLevelRepository : IEducationLevelRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public EducationLevelRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Add(EducationLevel educationLevel)
    {
        var eduLevelInDb = await _dbContext.EducationLevels
            .AsNoTracking()
            .FirstOrDefaultAsync(el => el.Id == educationLevel.Id);

        return await Result.SuccessIf(eduLevelInDb is null, educationLevel, 
            ResultError.Conflict("Education Level already exists"))
            .Tap(async edulevel => await _dbContext.EducationLevels.AddAsync(educationLevel))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ =>  Result.Success());
    }

    public async Task<Result> Update(EducationLevel educationLevel)
    {
        var exists = await _dbContext.EducationLevels.AnyAsync(el => el.Id == educationLevel.Id);

        return await Result.SuccessIf(exists, educationLevel,
                ResultError.NotFound($"Education Level with id {educationLevel.Id} not found"))
            .BindTry(async el =>
            {
                _dbContext.EducationLevels.Update(el);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            });
    }

    public async Task<Result<EducationLevel>> GetById(int id)
    {
        var educationLevel = await _dbContext.EducationLevels
            .AsNoTracking()
            .FirstOrDefaultAsync(el => el.Id == id);

        return Result.SuccessIf(educationLevel is null, educationLevel!, 
            ResultError.NotFound("Education Level not found"));
    }

    public async Task<Result> DeleteById(int id)
    {
        var educationLevel = await _dbContext.EducationLevels
            .FirstOrDefaultAsync(el => el.Id == id);

        return await Result.SuccessIf(educationLevel is not null, educationLevel!,
                ResultError.NotFound("Education Level not found"))
            .Tap(_ => _dbContext.EducationLevels.Remove(educationLevel))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }
}