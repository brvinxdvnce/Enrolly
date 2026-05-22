using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Authorize]
[Route("api/v1/managers")]
[ApiController]
public class ManagersController : ControllerBase
{
    private readonly IManagerService _managerService;

    public ManagersController(IManagerService managerService)
    {
        _managerService = managerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetManagers(
        [FromQuery] ManagerGrade? grade)
    {
        return Ok(await _managerService.GetManagersAsync(grade));
    }
    
    [HttpGet("{managerId:guid}")]
    public async Task<IActionResult> GetManager(
        [FromRoute] Guid managerId)
    {
        return Ok(await _managerService.GetManagerByIdAsync(managerId));
    }
    
    [HttpPost("{managerId:guid}")]
    public async Task<IActionResult> CreateManager(
        [FromRoute] Guid managerId,
        [FromBody] ManagerDto dto)
    {
        return Ok(await _managerService.CreateManagerAsync(managerId, dto));
    }
    
    [HttpPatch("{managerId:guid}")]
    public async Task<IActionResult> UpdateManagerInfo(
        [FromRoute] Guid managerId,
        [FromBody] ManagerDto dto)
    {
        await _managerService.UpdateManagerAsync(managerId, dto);
        return NoContent();
    }
    
    [HttpDelete("{managerId:guid}")]
    public async Task<IActionResult> DeleteManager(
        [FromRoute] Guid managerId)
    {
        await _managerService.DeleteManagerAsync(managerId);
        return NoContent();
    }
    
    [HttpPost("{managerId:guid}/promote")]
    public async Task<IActionResult> Promote(
        [FromRoute] Guid managerId)
    {
        await _managerService.PromoteAsync(managerId);
        return NoContent();
    }
    
    [HttpPost("{managerId:guid}/demote")]
    public async Task<IActionResult> Demote(
        [FromRoute] Guid managerId)
    {
        await _managerService.DemoteAsync(managerId);
        return NoContent();
    }
}