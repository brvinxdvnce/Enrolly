using System.Runtime.InteropServices.JavaScript;
using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Enums;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class AdmissionRepository : IAdmissionRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public AdmissionRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IReadOnlyList<Admission>>> GetAll()
    {
        var admissions = await _dbContext.Admissions
            .AsNoTracking()
            .ToListAsync();

        return Result.Success<IReadOnlyList<Admission>>(admissions);
    }

    public async Task<Result<ICollection<Admission>>> GetByApplicantId(Guid applicantId)
    {
        var admissions = await _dbContext.Admissions
            .Where(a => a.ApplicantId == applicantId)
            .ToListAsync<Admission>();

        return Result.Success<ICollection<Admission>>(admissions);
    }

    public async Task<Result<PagedResponce<Admission>>> GetMany(
        string? applicantName, 
        string? program,
        string? faculty,
        AdmissionStatus? status, 
        bool? isManaged,
        Guid? managerId,
        OrderDirection? lastUpdateSortDirection, 
        int page,
        int pageSize)
    {
        var admissions = 
            _dbContext.Admissions
                .AsNoTracking()
                .Include(a => a.Applicant)
                .Include(a => a.Programs)!
                .ThenInclude(ap => ap.Program)
                .ThenInclude(p => p.Faculty)
                .AsQueryable();

        // add ThenInclude later plz dont forget !!!!!!!!!!!!!!!!
        
        if (applicantName is not null)
            admissions = admissions.Where(a => a.Applicant.Name.Contains(applicantName));
        
        if (program is not null)
            admissions = admissions.Where(a =>
                a.Programs.Any(p =>
                    p.Program.Name.Contains(program)));
        
        if (faculty is not null)
            admissions = admissions.Where(a => 
                a.Programs.Any(p => 
                    p.Program.Faculty.Name.Contains(faculty)));
        
        if (status is not null)
            admissions = admissions.Where(a => a.AdmissionStatus == status);

        if (isManaged is not null)
            admissions = admissions.Where(a => a.ManagerId != null);
        
        if (managerId is not null)
            admissions = admissions.Where(a => a.ManagerId == managerId);

        if (lastUpdateSortDirection is not null)
        {
            if (lastUpdateSortDirection == OrderDirection.Ascending)
                admissions = admissions.OrderBy(a => a.LastUpdateTime);
            else admissions = admissions.OrderByDescending(a => a.LastUpdateTime);
        }

        var filteredAdmissionsCount = await admissions.CountAsync();
        
        admissions = admissions
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
        
        var materializedAdmissions = await admissions.ToListAsync();
        
        PagedResponce<Admission> pagedResponce = new PagedResponce<Admission>()
        {
            Content = materializedAdmissions,
            TotalCount = filteredAdmissionsCount,
            PageNumber = page,
            PageSize = pageSize,
            PagesCount = (int) Math.Ceiling( (double) filteredAdmissionsCount / pageSize )
        };
        
        return Result.Success(pagedResponce);
    }

    public async Task<Result<Guid>> Add(Admission admission)
    {
        var exists = await _dbContext.Admissions
            .AnyAsync(a => a.Id == admission.Id);
        
        if (exists)
            return Result.Failure<Guid>(ResultError.Conflict("Admission already exists"));
        
        var user = await _dbContext.Applicants
            .FirstOrDefaultAsync(u => u.Id == admission.ApplicantId);
        
        if (user is null)
            return Result.Failure<Guid>(ResultError.NotFound("Applicant not found"));
        
        user.Admissions.Add(admission);
        
        await _dbContext.SaveChangesAsync();
        
        return Result.Success<Guid>(admission.Id);
    }

    public async Task<Result<Admission>> GetById(Guid id)
    {
        var admission = await _dbContext.Admissions
            .Include(a => a.Programs)
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return Result.SuccessIf(admission is not null, admission!, $"Admissions with id {id} not found");
    }

    public async Task<Result> DeleteById(Guid id)
    {
        var admission = await _dbContext.Admissions.FirstOrDefaultAsync(a => a.Id == id);

        if (admission is null) return Result.Failure($"Admissions with id {id} not found");
        
        _dbContext.Admissions.Remove(admission);
        await  _dbContext.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result> AppointManager(Guid admissionId, Guid managerId)
    {
        var admission = await _dbContext.Admissions
            .FirstOrDefaultAsync(a => a.Id == admissionId);
        
        return await Result
            .SuccessIf(admission is not null,
                ResultError.NotFound($"Admissions with id {admissionId} not found"))
            .Ensure(() => admission!.ManagerId == null,
                ResultError.Conflict("Admission already had a manager"))
            .Tap(() => admission!.ManagerId = managerId)
            .Tap(() => admission!.LastUpdateTime = DateTime.UtcNow)
            .Tap(async () => { await _dbContext.SaveChangesAsync(); });
    }

    public async Task<Result> DismissManager(Guid admissionId)
    {
        var admission = await _dbContext.Admissions
            .FirstOrDefaultAsync(a => a.Id == admissionId);

        return await Result
            .SuccessIf(admission is not null,
                ResultError.NotFound($"Admissions with id {admissionId} not found"))
            .Tap(() => admission!.ManagerId = null)
            .Tap(() => admission!.LastUpdateTime = DateTime.UtcNow)
            .Tap(async () => await _dbContext.SaveChangesAsync());
    }

    public async Task<Result<Applicant>> GetApplicant(Guid admissionId)
    {
        var admission = await _dbContext.Admissions.AsNoTracking().FirstOrDefaultAsync(a => a.Id == admissionId);
        
        if (admission is null) 
            return Result.Failure<Applicant>(ResultError.NotFound("Admission not found"));
        
        var applicant = await _dbContext.Applicants
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == admission.ApplicantId);
        
        return Result.SuccessIf(applicant is not null, applicant!, ResultError.NotFound("Applicant not found"));
    }

    public async Task<Result> ChangeAdmissionStatus(Guid admissionId, AdmissionStatus status)
    {
        var admission = await _dbContext.Admissions
            .FirstOrDefaultAsync(a => a.Id == admissionId);
        
        return await Result.SuccessIf(admission is not null, 
            ResultError.NotFound("Admission not found"))
            .Tap(() => admission!.AdmissionStatus = status)
            .Tap(() => admission!.LastUpdateTime = DateTime.UtcNow)
            .Tap(async () => await _dbContext.SaveChangesAsync());
    }
}