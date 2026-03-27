/*using Microsoft.AspNetCore.Mvc;

namespace Enrolly.Accounts.Presentation.Controllers;

[Route("api/v1/users")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpGet]
    [Route("{id:guid}")]
    [Route("me")]
    public async Task<IActionResult> GetUser(
        [FromRoute] Guid? id)
    {
        return Ok();
    }
    
    [HttpPatch]
    [Route("{id:guid}")]
    [Route("me")]
    public async Task<IActionResult> ChangeUserInfo(
        [FromRoute] Guid? id)
    {
        return Ok();
    }

}*/


using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/v1/users")]
public class UsersController : ControllerBase
{
        
    
    public async Task<IActionResult> UpgradeRole()
    {
        return Ok();
    }
}