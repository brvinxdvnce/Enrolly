using Enrolly.EduDictionary.Application.Database;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Enrolly.EduDictionary.Application.Repositories;

public class ImportSummaryRepository : IImportSummaryRepository
{
    private readonly DictionaryDbContext _dbContext;

    public ImportSummaryRepository(DictionaryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<ImportSummary>> GetImportHistoryAsync(DateTime? from, DateTime? to)
    {
        var query = _dbContext.Imports.AsQueryable().AsNoTracking();
        
        if (from.HasValue)
            query = query.Where(i => i.StartedAt >= from);
        
        if (to.HasValue)
            query = query.Where(i => i.CompletedAt <= to);

        return await query.OrderByDescending(i => i.CompletedAt).ToListAsync();
    }

    public async Task<List<ImportSummary>> GetLastImportAsync()
    {
        
        return new List<ImportSummary>()
        {
            await _dbContext.Imports
                .AsNoTracking()
                .Where(i => i.CollectionName == "DocumentType")
                .OrderByDescending(i => i.CompletedAt)
                .FirstOrDefaultAsync(),
            
            await _dbContext.Imports
                .AsNoTracking()
                .Where(i => i.CollectionName == "Faculty")
                .OrderByDescending(i => i.CompletedAt)
                .FirstOrDefaultAsync(),

            await _dbContext.Imports
                .AsNoTracking()
                .Where(i => i.CollectionName == "Program")
                .OrderByDescending(i => i.CompletedAt)
                .FirstOrDefaultAsync(),

            await _dbContext.Imports
                .AsNoTracking()
                .Where(i => i.CollectionName == "EducationLevel")
                .OrderByDescending(i => i.CompletedAt)
                .FirstOrDefaultAsync()
        };
    }
}