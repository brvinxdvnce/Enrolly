using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;
using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Accounts.Infrastructure.Database;
using Enrolly.Contracts.Events.Events;
using Enrolly.Shared.Logging.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Accounts.Infrastructure.Repositories;

public class ManagerRepository : IManagerRepository
{
    private readonly UsersDbContext _dbContext;

    public ManagerRepository(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateManagerAsync(Guid id, Manager dto)
    {
        var user = await _dbContext.Users
                   .Include(u => u.ManagerProfile)
                   .FirstOrDefaultAsync(x => x.Id == id) 
                   ?? throw new NotFoundException("User not found");

        if (user.ManagerProfile is not null)
            throw new InvalidOperationException("Manager profile is already created");
        
        user.ManagerProfile = dto;
        user.ManagerProfile.AddEvent(new ManagerRegisteredEvent(user.Id, user.Email, user.UserName));
        
        await _dbContext.SaveChangesAsync();
        return user.ManagerProfile.Id;
    }

    public async Task<Manager?> GetManagerByIdAsync(Guid id)
    {
        return await _dbContext.Managers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Manager?>?> GetManagersAsync(ManagerGrade? grade)
    {
        var query = _dbContext.Managers.AsQueryable().AsNoTracking();
        
        if (grade is not null)
            query = query.Where(m => m.Grade == grade);

        return await query.ToListAsync();
    }

    public async Task UpdateManagerAsync(Guid id, Manager dto)
    {
        var manager = _dbContext.Managers.FirstOrDefault(x => x.Id == id);
        _dbContext.Entry(manager).CurrentValues
            .SetValues(new { Grade = dto.Grade, FacultyId  = dto.FacultyId });
    
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteManagerAsync(Guid id)
    {
        var manager = await _dbContext.Managers.FirstOrDefaultAsync(x => x.Id == id);
        if (manager is null)
            throw new NotFoundException();
        
        manager.AddEvent(new ManagerDeletedEvent(manager.Id));
        
        _dbContext.Managers.Remove(manager);
        await _dbContext.SaveChangesAsync();
    }

    public async Task SetRoleAsync(Guid id, ManagerGrade grade)
    {
        var manager = await _dbContext.Managers.FirstOrDefaultAsync(x => x.Id == id);
        manager.Grade =  grade;
        await _dbContext.SaveChangesAsync();
    }
}