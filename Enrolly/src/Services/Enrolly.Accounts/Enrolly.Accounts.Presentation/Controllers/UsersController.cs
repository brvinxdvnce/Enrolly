using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
    private readonly IUserProfileService _profileService;
    private readonly UserManager<User> _userManager;
    
    public UsersController(UserManager<User> userManager, IUserProfileService profileService)
    {
        _userManager = userManager;
        _profileService = profileService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetUser(
        [FromRoute] Guid id)
    {
        return Ok(await _profileService.GetUserAsync(id));
    }
    
    [HttpPatch("{id:guid}")]
    public async Task<IActionResult> UpdateInfo(
        [FromRoute] Guid id,
        [FromBody] UpdateUserDto updateUser)
    {
        await _profileService.UpdateUserAsync(id, updateUser);
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAccount(
        [FromRoute] Guid id)
    {
        await _profileService.DeleteUserAsync(id);
        return NoContent();
    }
}