using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class ProgramRepository : IProgramRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public ProgramRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(Program program)
    {
        var programExists = await _dbContext.Programs
            .AsNoTracking()
            .AnyAsync(p => p.Id == program.Id);

        /*if (program.Faculty is not null)
            _dbContext.Attach(program.Faculty);
        
        if (program.EducationLevel is not null)
            _dbContext.Attach(program.EducationLevel);*/
        
        return await Result.SuccessIf(!programExists, program!,
                ResultError.Conflict("Program already exists"))
            .Tap(async program => await _dbContext.Programs.AddAsync(program))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(program => Result.Success(program.Id));
    }

    public async Task<Result> Update(Program program)
    {
        var exists = await _dbContext.Programs.AnyAsync(p => p.Id == program.Id);

        return await Result.SuccessIf(exists, program,
                ResultError.NotFound($"Program with id {program.Id} not found"))
            .BindTry(async p =>
            {
                p.Faculty = null;
                p.EducationLevel = null;
                _dbContext.Programs.Update(p);
                await _dbContext.SaveChangesAsync();
                return Result.Success();
            });
    }

    public async Task<Result<Program>> GetById(Guid id)
    {
        var program = await _dbContext.Programs
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        return Result.SuccessIf(program is not null, program!,
            ResultError.NotFound("Program not found"));
    }

    public async Task<Result> DeleteById(Guid id)
    {
        var programInDb = await _dbContext.Programs
            .FirstOrDefaultAsync(p => p.Id == id);
        
        return await Result.SuccessIf(programInDb is not null, programInDb, 
            ResultError.NotFound("Program not found"))
            .Tap(program => _dbContext.Programs.Remove(program))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_=> Result.Success());
    }
}