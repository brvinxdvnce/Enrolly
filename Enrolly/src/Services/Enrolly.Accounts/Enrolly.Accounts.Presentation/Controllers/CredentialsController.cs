using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Route("api/v1/auth/credentials")]
[ApiController]
public class CredentialsController : ControllerBase
{
    [HttpPatch("password")]
    public async Task<IActionResult> ChangePassword()
    {
        return Ok();
    }
    
    [HttpPatch("email")]
    public async Task<IActionResult> ChangeEmail()
    {
        return Ok();
    }
}