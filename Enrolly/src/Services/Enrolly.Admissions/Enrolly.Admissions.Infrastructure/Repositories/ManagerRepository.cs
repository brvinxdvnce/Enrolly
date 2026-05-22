using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Models;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public ManagerRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Guid>> Add(Manager manager)
    {
        var exists = await _dbContext.Managers
            .AnyAsync(m => m.Id == manager.Id);

        return await Result.SuccessIf(!exists, manager,
            ResultError.Conflict($"Manager with id {manager.Id} already exists"))
            .Tap(m => _dbContext.Managers.Add(manager))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success(manager.Id));
    }

    public async Task<Result<Manager>> GetById(Guid managerId)
    {
        var manager = await _dbContext.Managers
            .FirstOrDefaultAsync(m => m.Id == managerId);

        return Result.SuccessIf(manager is not null, manager!,
            ResultError.NotFound($"Manager with id {managerId} not found"));
    }

    public async Task<Result> DeleteById(Guid managerId)
    {
        var manager = await _dbContext.Managers
            .FirstOrDefaultAsync(m => m.Id == managerId);
        
        return await Result.SuccessIf(manager is not null, manager!,
            ResultError.NotFound($"Manager with id {managerId} not found"))
            .Tap(m => _dbContext.Managers.Remove(m))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }

    public async Task<Result<Manager>> Update(Manager manager)
    {
        throw new NotImplementedException();
    }

    public async Task<Result> ChangeGrade(Guid managerId, ManagerGrade grade)
    {
        var manager = await _dbContext.Managers
            .FirstOrDefaultAsync(m => m.Id == managerId);

        return await Result.SuccessIf(manager is not null, manager,
                ResultError.NotFound($"Manager with id {managerId} not found"))
            .Tap(m => m.Grade = grade)
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }
}