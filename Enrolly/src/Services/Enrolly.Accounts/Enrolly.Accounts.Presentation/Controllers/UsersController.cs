using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUserProfileService _profileService;
    
    public UsersController(IUserProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUser(
        [FromRoute] Guid userId)
    {
        return Ok(await _profileService.GetUserAsync(userId));
    }
    
    [HttpPatch("{userId:guid}")]
    public async Task<IActionResult> UpdateInfo(
        [FromRoute] Guid userId,
        [FromBody] UpdateUserDto updateUser)
    {
        await _profileService.UpdateUserAsync(userId, updateUser);
        return NoContent();
    }
    
    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteAccount(
        [FromRoute] Guid userId)
    {
        await _profileService.DeleteUserAsync(userId);
        return NoContent();
    }
}