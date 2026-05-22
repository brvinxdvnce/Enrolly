using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class EducationDocumentRepositoryV2 : IEducationDocumentRepositoryV2
{
    private readonly DocumentsDbContext _dbContext;

    public EducationDocumentRepositoryV2(DocumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<IReadOnlyCollection<EducationDocument>>> GetAllByUserIdAsync(Guid userId)
    {
        var documents = await _dbContext.Diplomas
            .AsNoTracking()
            .Where(d => d.ApplicantId == userId)
            .ToListAsync();

        return Result.Success((IReadOnlyCollection<EducationDocument>) documents);
    }

    public async Task<Result<EducationDocument>> GetByIdAsync(Guid documentId)
    {
        var document = await _dbContext.Diplomas
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == documentId);
        
        return Result.SuccessIf(document is not null, document!,
            ResultError.NotFound($"Document with id {documentId} not found"));
    }

    public async Task<Result<Guid>> CreateAsync(EducationDocument educationDocument)
    {
        var exists = await _dbContext.Diplomas
            .AnyAsync(d => d.Id == educationDocument.Id);

        return await Result.SuccessIf(!exists, educationDocument,
            ResultError.Conflict($"Document with id {educationDocument.Id} already exists"))
            .Tap(async eduDoc => await _dbContext.Diplomas.AddAsync(eduDoc))
            .Tap(async eduDoc => await _dbContext.SaveChangesAsync())
            .Bind(eduDoc => Result.Success(eduDoc.Id));
    }

    public async Task<Result> UpdateAsync(EducationDocument educationDocument)
    {
        return await Result.Try(async () => {
            _dbContext.Diplomas.Update(educationDocument);
            await _dbContext.SaveChangesAsync();
        }, ex => ResultError.Internal(ex.Message));
    }

    public async Task<Result> DeleteAsync(Guid documentId)
    {
        var document = await _dbContext.Diplomas
            .FirstOrDefaultAsync(d => d.Id == documentId);
        
        return await Result.SuccessIf(document is not null, document!,
            ResultError.NotFound($"Document with id {documentId} not found"))
            .Tap(doc => _dbContext.Diplomas.Remove(doc))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }
}