using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

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
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetManager(
        [FromRoute] Guid id)
    {
        return Ok(await _managerService.GetManagerByIdAsync(id));
    }
    
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> CreateManager(
        [FromRoute] Guid id,
        [FromBody] ManagerDto dto)
    {
        return Ok(await _managerService.CreateManagerAsync(id, dto));
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateManagerInfo(
        [FromRoute] Guid id,
        [FromBody] ManagerDto dto)
    {
        await _managerService.UpdateManagerAsync(id, dto);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteManager(
        [FromRoute] Guid id)
    {
        await _managerService.DeleteManagerAsync(id);
        return NoContent();
    }
    
    [HttpPost("{id:guid}/promote")]
    public async Task<IActionResult> Promote(
        [FromRoute] Guid id)
    {
        await _managerService.PromoteAsync(id);
        return NoContent();
    }
    
    [HttpPost("{id:guid}/demote")]
    public async Task<IActionResult> Demote(
        [FromRoute] Guid id)
    {
        await _managerService.DemoteAsync(id);
        return NoContent();
    }
}