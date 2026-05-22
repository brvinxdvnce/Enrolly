using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class EducationDocumentTypeRepository : IEducationDocumentTypeRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public EducationDocumentTypeRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Add(EducationDocumentType documentType)
    {
        var exists = await _dbContext.DocumentTypes.AnyAsync(x => x.Id == documentType.Id);
        if (exists) return Result.Failure(ResultError.Conflict($"Document Type with Id {documentType.Id} already exists"));
        
        _dbContext.ChangeTracker.Clear();
        /*var nextEduLevels = await _dbContext.EducationLevels
            .Where(el => documentType.NextEducationLevelIds.Contains(el.Id))
            .ToListAsync();

        foreach (var el in nextEduLevels)
            documentType.NextEducationLevels.Add(el);*/

        await _dbContext.DocumentTypes.AddAsync(documentType);
        
        await _dbContext.SaveChangesAsync();
        return Result.Success();
    }

    public async Task<Result> Update(EducationDocumentType documentType)
    {
        Console.WriteLine("hello world");
        return Result.Success();
    }

    public async Task<Result<EducationDocumentType>> GetById(Guid documentTypeId)
    {
        var doc = await _dbContext.DocumentTypes
            .FirstOrDefaultAsync(x => x.Id == documentTypeId);

        return Result.SuccessIf(doc is not null, doc!,
            ResultError.NotFound("Document type not found"));
    }

    public async Task<Result> DeleteById(Guid documentTypeId)
    {
        var documentType = await _dbContext.DocumentTypes
            .FirstOrDefaultAsync(d => d.Id == documentTypeId);

        return await Result.SuccessIf(documentType is not null, documentType!,
                ResultError.NotFound($"Document type with Id {documentTypeId} does not exist."))
            .Tap(doc => _dbContext.DocumentTypes.Remove(doc))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }
}