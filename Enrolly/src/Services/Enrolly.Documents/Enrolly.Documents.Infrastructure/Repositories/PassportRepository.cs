using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class PassportRepository : IPassportRepository
{
    private readonly ILogger<PassportRepository> _logger;
    private readonly DocumentsDbContext _dbContext;

    public PassportRepository(DocumentsDbContext dbContext, ILogger<PassportRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Passport?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Passports
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Guid> CreateAsync(Passport passport)
    {
        _logger.LogError("Creating passport");
            
        var user = await _dbContext.Applicants.FirstOrDefaultAsync(u => u.Id == passport.Id);
        
        if (user == null) throw new InvalidOperationException();
        
        user.Passport = passport;
        
        await _dbContext.SaveChangesAsync();
        
        return passport.Id;
    }

    public async Task UpdateAsync(Passport passport)
    {
        var currPassport = await _dbContext.Passports
            .FirstOrDefaultAsync(p => p.Id == passport.Id)
            ?? throw new InvalidOperationException();
       
        currPassport.Fullname = passport.Fullname?? currPassport.Fullname;
        currPassport.DepartmentCode = passport.DepartmentCode ?? currPassport.DepartmentCode;
        currPassport.Registration = passport.Registration ?? currPassport.Registration;
        currPassport.Series = passport.Series ?? currPassport.Series;
        currPassport.Number = passport.Number ?? currPassport.Number;
        currPassport.IssueDate = passport.IssueDate;
        currPassport.IssuedBy = passport.IssuedBy ?? currPassport.IssuedBy;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid id)
    {
        var passport = new Passport { Id = id };
        _dbContext.Passports.Attach(passport);
        _dbContext.Passports.Remove(passport);
        await _dbContext.SaveChangesAsync();
    }
}