using System.Runtime;
using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Models;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly DocumentsDbContext _dbContext;
    
    public async Task<Result<Guid>> Add(Manager manager)
    {
        var exists = await _dbContext.Managers
            .AnyAsync(m => m.Id == manager.Id);

        return await Result.SuccessIf(!exists, manager,
                ResultError.Conflict($"Manager with Id {manager.Id} already exists"))
            .Tap(m => _dbContext.Managers.Add(manager))
            .Tap(async m => await _dbContext.SaveChangesAsync())
            .Bind(m => Result.Success(m.Id));
    }

    public async Task<Result<Manager>> GetById(Guid managerId)
    {
        var manager = await _dbContext.Managers
            .FirstOrDefaultAsync(m => m.Id == managerId);

        return Result.SuccessIf(manager is not null, manager!,
            ResultError.NotFound($"Manager with Id {managerId} does not exist"));
    }

    public async Task<Result> DeleteById(Guid managerId)
    {
        var manager = await _dbContext.Managers
            .FirstOrDefaultAsync(m => m.Id == managerId);
        
        return await Result.SuccessIf(manager is not null, manager!,
            ResultError.NotFound($"Manager with Id {managerId} does not found."))
            .Tap(m => _dbContext.Managers.Remove(m))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }

    public async Task<Result> Update(Manager manager)
    {
        return await Result.Try(async () =>
        {
            _dbContext.Managers.Update(manager);
            await _dbContext.SaveChangesAsync();
        }, ex => ResultError.Internal(ex.Message));
    }

    public async Task<Result> ChangeGrade(Guid managerId, ManagerGrade grade)
    {
        var manager = await _dbContext.Managers
            .FirstOrDefaultAsync(m => m.Id == managerId);
        
        return await Result.SuccessIf(manager is not null, manager!,
            ResultError.NotFound($"Manager with Id {managerId} not found."))
            .Tap(m => m.Grade = grade)
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(m => Result.Success());
    }
}