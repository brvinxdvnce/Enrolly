using Enrolly.AdminClient.Models;
using Enrolly.AdminClient.Models.ViewModels;
using Enrolly.AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.AdminClient.Controllers;

public class DictionaryController : Controller
{
    private readonly DictionaryService _dictionaryService;

    public DictionaryController(DictionaryService dictionaryService)
    {
        _dictionaryService = dictionaryService;
    }
    
    public async Task<IActionResult> Faculties()
    {
        var vm = new DictionaryViewModel
        {
            Faculties = await _dictionaryService.GetFacultiesAsync()
        };
        return View(vm);
    }
    
    public async Task<IActionResult> EducationLevels()
    {
        var vm = new DictionaryViewModel
        {
            EducationLevels = await _dictionaryService.GetEducationLevelsAsync()
        };
        return View(vm);
    }
    
    public async Task<IActionResult> DocumentTypes()
    {
        var vm = new DictionaryViewModel
        {
            DocumentTypes = await _dictionaryService.GetDocumentTypesAsync()
        };
        return View(vm);
    }
    
    public async Task<IActionResult> Programs(
        Guid? facultyId, 
        int? educationLevelId,
        string? educationForm,
        string? language,
        string? programName,
        string? programCode,
        int page = 1,
        int pageSize = 10)
    {
        var vm = new ProgramsViewModel
        {
            FacultyId = facultyId,
            EducationLevelId = educationLevelId,
            EducationForm = educationForm,
            Language = language,
            ProgramName = programName,
            ProgramCode = programCode,
            Page = page,
            PageSize = pageSize
        };
 
        var facultiesTask  = await _dictionaryService.GetFacultiesAsync();
        var levelsTask     = await _dictionaryService.GetEducationLevelsAsync();
        var programsTask   = await _dictionaryService.GetProgramsAsync(
            facultyId, 
            educationLevelId,
            educationForm,
            language,
            programName,
            programCode,
            page,
            pageSize);
 
        var faculties = facultiesTask;
        var levels    = levelsTask;
        var programs  = programsTask;
 
        vm.Faculties = faculties;
        vm.EducationLevels = levels;
        vm.Programs = programs;
 
        return View(vm);
    }
}