using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class FacultyRepository : IFacultyRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public FacultyRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<Faculty>> Add(Faculty faculty)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<Faculty>> GetById(Guid id)
    {
        var faculty = await _dbContext.Faculties.FirstOrDefaultAsync(x => x.Id == id);

        return Result.SuccessIf(faculty is not null, faculty!, ResultError.NotFound("Faculty not found"));
    }

    public async Task<Result> DeleteById(Guid facultyId)
    {
        var faculty = await _dbContext.Faculties.FirstOrDefaultAsync(f => f.Id == facultyId);

        return await Result.SuccessIf(faculty is not null, faculty!, 
            ResultError.NotFound("Faculty not found"))
            .Tap(f => _dbContext.Faculties.Remove(f))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }
}