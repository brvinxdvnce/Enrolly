using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class AdmissionProgramRepository : IAdmissionProgramRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public AdmissionProgramRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Add(Guid admissionId, Guid programId, int programPriority)
    {
        var admission = await _dbContext.Admissions
            .Include(a => a.Programs)
            .FirstOrDefaultAsync(a => a.Id == admissionId);
        
        if (admission is null)
            return Result.Failure(ResultError.NotFound("Admission not found"));
        
        var program = await _dbContext.Programs.FirstOrDefaultAsync(p => p.Id == programId);
        
        if (program is null)
            return Result.Failure(ResultError.NotFound("Program not found"));
        
        if (admission.Programs.Select(p => p.ProgramId).Contains(programId))
            return Result.Failure(ResultError.Conflict("Program already added to admission"));

        var admissionProgram = new AdmissionProgram(admissionId, programId, programPriority);
        
        admission.Programs.Add(admissionProgram);
        
        await _dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public Task<Result<AdmissionProgram>> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> RemoveById(Guid admissionId, Guid programId)
    {
        var admission = await _dbContext.Admissions
            .Include(a => a.Programs)
            .FirstOrDefaultAsync(a => a.Id == admissionId);
        
        return await Result.SuccessIf(admission is not null,
            ResultError.NotFound("Admission not found"))
            .Bind(() => Result.Success(admission!))
            .Ensure(a => a.Programs.Any(p => p.ProgramId == programId),
                ResultError.NotFound("Program not found in admission"))
            .Tap(a => a.Programs.Remove(a.Programs.First(p => p.ProgramId == programId)))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }

    public async Task<Result> ChangeProgramPriority(Guid admissionId, Guid programId, int programPriority)
    {
        var admission = await _dbContext.Admissions
            .Include(a => a.Programs)
            .FirstOrDefaultAsync(a => a.Id == admissionId);

        return await Result.SuccessIf(admission is not null,
                ResultError.NotFound("Admission not found"))
            .Bind(() => Result.Success(admission!))
            .Ensure(a => a.Programs.Any(p => p.ProgramId == programId),
                ResultError.NotFound("The program has not been added to the application list."))
            .Tap(a => 
                a.Programs.First(p => 
                    p.ProgramId == programId).Priority = programPriority)
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }
}
