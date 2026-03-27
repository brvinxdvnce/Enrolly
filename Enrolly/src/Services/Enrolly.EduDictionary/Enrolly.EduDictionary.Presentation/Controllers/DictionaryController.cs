using Enrolly.EduDictionary.Application.Repositories;
using Enrolly.EduDictionary.Domain.Repositories;
using Enrolly.EduDictoinary.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.EduDictionary.Presentation.Controllers;

[ApiController]
[Route("api/v1/dictionary")]
public class DictionaryController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IProgramRepository _programRepository;
    private readonly IEducationLevelRepository _educationLevelRepository;
    private readonly DocumentTypeRepository _documentTypeRepository;
    
    
    public DictionaryController(
        ILogger logger,
        IProgramRepository programRepository, IEducationLevelRepository educationLevelRepository, DocumentTypeRepository documentTypeRepository)
    {
        _logger = logger;
        _programRepository = programRepository;
        _educationLevelRepository = educationLevelRepository;
        _documentTypeRepository = documentTypeRepository;
    }
    
    [HttpGet]
    [Route("programs")]
    public async Task<IActionResult> GetPrograms(
        [FromQuery] List<string> faculties,
        [FromQuery] int level,
        [FromQuery] int mode,
        [FromQuery] string language,
        [FromQuery] string program,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
        )
    {
        return Ok();
    }

    [HttpGet]
    [Route("faculties")]
    public async Task<IActionResult> GetFaculties()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("edulevels")]
    public async Task<IActionResult> GetEducationLevels()
    {
        return Ok();
    }
    
    [HttpGet]
    [Route("doctypes")]
    public async Task<IActionResult> GetDocumentTypes()
    {
        return Ok();
    }
}