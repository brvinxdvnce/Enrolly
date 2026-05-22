using Enrolly.AdminClient.Models.ViewModels;
using Enrolly.AdminClient.Services;
using Microsoft.AspNetCore.Mvc;

namespace Enrolly.AdminClient.Controllers;

public class ImportsController : Controller
{
    private readonly ImportsService _importsService;

    public ImportsController(ImportsService importsService)
    {
        _importsService = importsService;
    }
    
    public async Task<IActionResult> Index(DateTime? from, DateTime? to)
    {
        var vm = new ImportsViewModel { From = from, To = to };
 
        vm.LastImport = await _importsService.GetLastImportAsync();
        vm.History = await _importsService.GetImportHistoryAsync(from, to);
 
        return View(vm);
    }
}