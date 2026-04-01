using Enrolly.EduDictionary.Application.Services.Interfaces;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Enrolly.Shared.Logging;

namespace Enrolly.EduDictionary.Presentation.Controllers;

[Route("api/v1/dictionary/imports")]
[ApiController]
public class ImportsController : ControllerBase
{
    private readonly ILogger<ImportsController> _logger;
    private readonly IImportSummaryRepository _importRepository;
    private readonly IExternalDataCollector _importCollector;
    
    public ImportsController(ILogger<ImportsController> logger, IImportSummaryRepository importRepository, IExternalDataCollector importCollector)
    {
        _logger = logger;
        _importRepository = importRepository;
        _importCollector = importCollector;
    }
    
    [HttpGet("history")]
    public async Task<IActionResult> GetImportHistory(
        [FromQuery] DateTime? from,
        [FromQuery] DateTime? to)
    {
        return Ok(await _importRepository.GetImportHistoryAsync(from.ToUtc(), to.ToUtc()));
    }

    [HttpGet]
    public async Task<IActionResult> GetLastImport()
    {
        return Ok(await _importRepository.GetLastImportAsync());
    }
    
    [HttpPost]
    public async Task<IActionResult> StartImport(
        [FromQuery] string import)
    {
        try
        {
            await (import switch
            {
                "Program" => _importCollector.ImportPrograms(),
                "Faculty" => _importCollector.ImportFaculties(),
                "DocumentType" => _importCollector.ImportDocumentTypes(),
                "EducationLevel" => _importCollector.ImportEducationLevels(),
                "All" => _importCollector.ImportAll(),
                _ => throw new ArgumentException("Invalid import argument: {import}", import)
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return BadRequest("Invalid arguments");
        }

        return Ok(await _importRepository.GetLastImportAsync());
    }
}