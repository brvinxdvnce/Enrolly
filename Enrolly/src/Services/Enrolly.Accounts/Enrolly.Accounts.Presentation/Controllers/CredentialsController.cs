using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Route("api/v1/auth/credentials")]
[ApiController]
public class CredentialsController : ControllerBase
{
    private readonly ICredentialsService _credentialsService;

    public CredentialsController(ICredentialsService credentialsService)
    {
        _credentialsService = credentialsService;
    }

    [HttpPatch("password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordRequestDto requestDto)
    {
        await _credentialsService.ResetPassword(requestDto);
        return NoContent();
    }
    
    [HttpPatch("email")]
    public async Task<IActionResult> ChangeEmail(
        [FromBody] ChangeEmailRequestDto requestDto)
    {
        await _credentialsService.ResetEmail(requestDto);
        return NoContent();
    }
}