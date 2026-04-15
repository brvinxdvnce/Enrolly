using Enrolly.Documents.Domain.Entities;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Domain.Repositories;

public interface IDiplomaRepository
{
    public Task<IEnumerable<EducationDocument?>> GetAllByUserIdAsync(Guid userId);
    public Task<EducationDocument?> GetByIdAsync(Guid id);
    public Task CreateAsync(EducationDocument educationDocument);
    public Task UpdateAsync(EducationDocument educationDocument);
    public Task DeleteAsync(Guid id);
}