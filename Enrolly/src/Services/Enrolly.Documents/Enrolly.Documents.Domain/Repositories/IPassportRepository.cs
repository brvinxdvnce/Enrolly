using Enrolly.Documents.Domain.Entities;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Domain.Repositories;

public interface IPassportRepository
{
    public Task<Passport?> GetByIdAsync(Guid id);
    public Task<Guid> CreateAsync(Passport passport);
    public Task UpdateAsync(Passport passport);
    public Task DeleteAsync(Guid id);
}