using Enrolly.Contracts.Events.Events;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Enrolly.Shared.Logging.Utils.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class EducationDocumentRepository : IEducationDocumentRepository
{
    private readonly DocumentsDbContext _dbContext;

    public EducationDocumentRepository(DocumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<EducationDocument>> GetAllByUserIdAsync(Guid userId)
    {
        return await _dbContext.Diplomas
            .AsNoTracking()
            .Where(d => d.ApplicantId == userId)
            .ToListAsync();
    }

    public async Task<EducationDocument?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Diplomas
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task CreateAsync(EducationDocument educationDocument)
    {
        _dbContext.Diplomas.Add(educationDocument);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(EducationDocument educationDocument)
    {
        var diplomaToUpdate = _dbContext.Diplomas
            .FirstOrDefault(d => d.Id == educationDocument.Id)
            ?? throw new NotFoundException("Document not found");
        
        _dbContext.Entry(diplomaToUpdate).CurrentValues.SetValues(educationDocument);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var currDiploma = await _dbContext.Diplomas
            .FirstOrDefaultAsync(d => d.Id == id);
        
        currDiploma.AddEvent(new DocumentDeletedEvent(currDiploma.ApplicantId, currDiploma.Id));
        _dbContext.Attach(currDiploma);
        _dbContext.Diplomas.Remove(currDiploma);
        
        await _dbContext.SaveChangesAsync();
    }
}