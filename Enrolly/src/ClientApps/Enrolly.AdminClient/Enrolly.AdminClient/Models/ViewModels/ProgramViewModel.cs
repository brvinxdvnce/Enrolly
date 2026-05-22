using Enrolly.AdminClient.Models.Models;
using Enrolly.Shared.Logging;

namespace Enrolly.AdminClient.Models.ViewModels;

public class ProgramsViewModel
{
    public PagedResponce<Enrolly.AdminClient.Models.Models.Program>? Programs { get; set; }
    public List<Faculty> Faculties { get; set; } = [];
    public List<EducationLevel> EducationLevels { get; set; } = [];
 
    // фильтры
    public Guid? FacultyId { get; set; }
    public int? EducationLevelId { get; set; }
    public string? EducationForm { get; set; }
    public string? Language { get; set; }
    public string? ProgramName { get; set; }
    public string? ProgramCode { get; set; }
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}