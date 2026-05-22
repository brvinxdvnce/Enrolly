using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Result;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class DocumentTypeRepository : IDocumentTypeRepository
{
    private readonly DocumentsDbContext _dbContext;

    public DocumentTypeRepository(DocumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result> Add(EducationDocumentType documentType)
    {
        var exists = await _dbContext.EducationDocumentTypes
            .AnyAsync(edt => edt.Id == documentType.Id);
        
        return await Result.SuccessIf(!exists, documentType,
            ResultError.Conflict($"Document type with id {documentType.Id} already exist"))
            .Tap(edt => _dbContext.EducationDocumentTypes.Add(edt))
            .Tap(async _ => await _dbContext.SaveChangesAsync())
            .Bind(_ => Result.Success());
    }

    public async Task<Result> Update(EducationDocumentType documentType)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<EducationDocumentType>> GetById(Guid documentTypeId)
    {
        var documentType = await _dbContext.EducationDocumentTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(edt => edt.Id == documentTypeId);

        return Result.SuccessIf(documentType is not null, documentType!,
            ResultError.NotFound($"Document type with id {documentTypeId} does not exist"));
    }

    public async Task<Result> DeleteById(Guid documentTypeId)
    {
        var documentType = await _dbContext.EducationDocumentTypes
            .FirstOrDefaultAsync(edt => edt.Id == documentTypeId);
        
        return await Result.SuccessIf(documentType is not null, documentType!,
            ResultError.NotFound($"Document type with id {documentTypeId} does not exist"))
            .Tap(doc => _dbContext.EducationDocumentTypes.Remove(doc))
            .Tap(async _ => await _dbContext.SaveChangesAsync());
    }
}