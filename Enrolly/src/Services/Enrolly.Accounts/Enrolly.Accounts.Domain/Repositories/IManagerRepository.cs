using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;

namespace Enrolly.Accounts.Domain.Repositories;

public interface IManagerRepository
{
    public Task<Guid> CreateManagerAsync(Guid id, Manager dto);
    public Task<Manager?> GetManagerByIdAsync(Guid id);
    public Task<IEnumerable<Manager?>?> GetManagersAsync(ManagerGrade? role);
    public Task UpdateManagerAsync(Guid id, Manager dto);
    public Task DeleteManagerAsync(Guid id);
    public Task SetRoleAsync(Guid id, ManagerGrade grade);
}