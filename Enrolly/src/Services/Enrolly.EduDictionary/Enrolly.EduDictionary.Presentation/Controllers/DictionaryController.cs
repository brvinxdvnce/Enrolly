using Enrolly.EduDictionary.Application.Repositories;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.EduDictionary.Presentation.Controllers;

[ApiController]
[Route("api/v1/dictionary")]
public class DictionaryController : ControllerBase
{
    private readonly ILogger<DictionaryController> _logger;
    private readonly IProgramRepository _programRepository;
    private readonly IEducationLevelRepository _educationLevelRepository;
    private readonly IDocumentTypeRepository _documentTypeRepository;
    private readonly IFacultyRepository _facultyRepository;
    
    public DictionaryController(
        ILogger<DictionaryController> logger,
        IProgramRepository programRepository, 
        IEducationLevelRepository educationLevelRepository, 
        IDocumentTypeRepository documentTypeRepository,
        IFacultyRepository facultyRepository)
    {
        _logger = logger;
        _programRepository = programRepository;
        _educationLevelRepository = educationLevelRepository;
        _documentTypeRepository = documentTypeRepository;
        _facultyRepository = facultyRepository;
    }
    
    [HttpGet]
    [Route("programs")]
    public async Task<IActionResult> GetPrograms(
        [FromQuery] Guid? facultyId,
        [FromQuery] int? educationLevelId,
        [FromQuery] string? educationForm,
        [FromQuery] string? language,
        [FromQuery] string? programName,
        [FromQuery] string? programCode,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10
        )
    {
        return Ok(await _programRepository.GetPrograms(
            facultyId,
            educationLevelId,
            educationForm,
            language,
            programName,
            programCode,
            page,
            pageSize));
    }

    [HttpGet]
    [Route("faculties")]
    public async Task<IActionResult> GetFaculties()
    {
        return Ok(await _facultyRepository.GetFaculties());
    }
    
    [HttpGet]
    [Route("edulevels")]
    public async Task<IActionResult> GetEducationLevels()
    {
        return Ok(await _educationLevelRepository.GetEducationLevels());
    }
    
    [HttpGet]
    [Route("doctypes")]
    public async Task<IActionResult> GetDocumentTypes()
    {
        return Ok(await _documentTypeRepository.GetDocumentTypes());
    }
}