using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Mappers;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Enums;
using Enrolly.Accounts.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Application.Services.Implementations;

public class ManagerService : IManagerService
{
    private readonly ManagerMapper _mapper;
    private readonly ILogger<ManagerService> _logger;
    private readonly IManagerRepository _managerRepository;

    public ManagerService(ManagerMapper mapper, IManagerRepository managerRepository, ILogger<ManagerService> logger)
    {
        _mapper = mapper;
        _logger = logger;
        _managerRepository = managerRepository;
    }

    public async Task<Guid> CreateManagerAsync(Guid id, ManagerDto manager)
    {
        var managerModel = _mapper.FromDto(manager);
        return await _managerRepository.CreateManagerAsync(id, managerModel);
    }

    public async Task<ManagerDto> GetManagerByIdAsync(Guid id)
    {
        return _mapper.ToDto(await _managerRepository.GetManagerByIdAsync(id));
    }
    
    public async Task<IEnumerable<ManagerDto>> GetManagersAsync(ManagerGrade? grade)
    {
        return _mapper.ToDtos(await _managerRepository.GetManagersAsync(grade));
    }

    public async Task UpdateManagerAsync(Guid id, ManagerDto manager)
    {
        var managerModel = _mapper.FromDto(manager);
        await _managerRepository.UpdateManagerAsync(id, managerModel);
    }

    public async Task DeleteManagerAsync(Guid id)
    {
        await _managerRepository.DeleteManagerAsync(id);
    }

    public async Task PromoteAsync(Guid id)
    {
        var manager = await GetManagerByIdAsync(id);
        if (manager.Grade == ManagerGrade.GeneralManager) return;

        await _managerRepository.SetRoleAsync(id, ManagerGrade.GeneralManager);
    }

    public async Task DemoteAsync(Guid id)
    {
        var manager = await _managerRepository.GetManagerByIdAsync(id);
        if (manager.Grade == ManagerGrade.DefaultManager) return;
        
        await _managerRepository.SetRoleAsync(id,  ManagerGrade.DefaultManager);
    }
}