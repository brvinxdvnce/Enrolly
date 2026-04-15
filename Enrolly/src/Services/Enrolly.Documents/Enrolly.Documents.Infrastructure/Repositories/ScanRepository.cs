using Enrolly.Documents.Domain.Repositories;
using Enrolly.Documents.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Infrastructure.Repositories;

public class ScanRepository : IScanRepository
{
    private readonly DocumentsDbContext _dbContext;

    public ScanRepository(DocumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<File> GetByIdAsync(Guid id)
    {
        return await _dbContext.Files
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Guid> AddScanAsync(File file)
    {
        _dbContext.Files.Add(file);
        await _dbContext.SaveChangesAsync();
        return file.Id;
    }

    public async Task RemoveScanAsync(Guid id)
    {
        var file = new File { Id = id};
        _dbContext.Attach(file);
        _dbContext.Files.Remove(file);
        await _dbContext.SaveChangesAsync();
    }
}