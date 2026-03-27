using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Route("api/v1/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok();
    }
    
    /*[HttpPost("register")]
    public async Task<IActionResult> Register()
    {
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok();
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken()
    {
        return Ok();
    }
    
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        return Ok();
    }*/
}