using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;

namespace Enrolly.Accounts.Application.Services.Interfaces;

public interface IManagerService
{
    public Task<Guid> CreateManagerAsync(Guid id, ManagerDto manager);
    public Task<ManagerDto> GetManagerByIdAsync(Guid id);
    public Task<IEnumerable<ManagerDto>> GetManagersAsync(ManagerGrade? grade);
    public Task UpdateManagerAsync(Guid id, ManagerDto manager);
    public Task DeleteManagerAsync(Guid id);
    public Task PromoteAsync(Guid id);
    public Task DemoteAsync(Guid id);
}