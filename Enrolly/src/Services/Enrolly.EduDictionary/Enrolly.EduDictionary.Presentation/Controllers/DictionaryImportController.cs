using Microsoft.AspNetCore.Mvc;

namespace Enrolly.EduDictionary.Presentation.Controllers;

[ApiController]
[Route("api/v1/dictionary/import")]
public class DictionaryImportController : ControllerBase
{
    [HttpGet]
    [Route("status")]
    public async Task<IActionResult> GetImportStatuses()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("status/tatest")]
    public async Task<IActionResult> GetLastImportStatus()
    {
        return Ok();
    }
    
    [HttpPost]
    public async Task<IActionResult> StartImport()
    {
        return Ok();
    }
}