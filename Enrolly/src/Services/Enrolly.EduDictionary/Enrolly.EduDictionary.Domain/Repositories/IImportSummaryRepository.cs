using Enrolly.EduDictionary.Domain.Entities;

namespace Enrolly.EduDictionary.Domain.Repositories;

public interface IImportSummaryRepository
{
    public Task<List<ImportSummary>> GetImportHistoryAsync(DateTime? from, DateTime? to);
    public Task<List<ImportSummary>> GetLastImportAsync();
}