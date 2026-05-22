using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Admissions.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Admissions.Infrastructure.Repositories;

public class EducationDocumentRepository : IDocumentRepository
{
    private readonly AdmissionsDbContext _dbContext;

    public EducationDocumentRepository(AdmissionsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> AddAsync(EducationDocument document)
    {
        var applicant = await _dbContext.Applicants
            .FirstOrDefaultAsync(a => a.Id == document.UserId);
        
        return await Result.SuccessIf(applicant is not null, applicant!, 
            ResultError.NotFound("Applicant not found"))
            .Tap(a => a.Documents.Add(document))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }

    public async Task<Result> DeleteAsync(Guid documentId)
    {
        var documentInDb = await _dbContext.Documents
            .FirstOrDefaultAsync(d => d.DocumentId == documentId);

        return await Result.SuccessIf(documentInDb is not null, documentInDb!,
                ResultError.NotFound("Document not found"))
            .Tap(_ => _dbContext.Documents.Remove(documentInDb))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_  => Result.Success()) ;
    }
}