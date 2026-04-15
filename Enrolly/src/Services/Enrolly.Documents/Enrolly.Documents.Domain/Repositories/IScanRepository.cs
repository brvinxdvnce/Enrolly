using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Domain.Repositories;

public interface IScanRepository
{
    public Task<File> GetByIdAsync(Guid id);
    public Task<Guid> AddScanAsync(File file);
    public Task RemoveScanAsync(Guid id);
}