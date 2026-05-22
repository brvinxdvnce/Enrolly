
using Enrolly.AdminClient.Models.Models;

namespace Enrolly.AdminClient.Models.ViewModels;

public class ImportsViewModel
{
    public ImportSummary? LastImport { get; set; }
    public List<ImportSummary> History { get; set; } = [];
    public DateTime? From { get; set; }
    public DateTime? To { get; set; }
    public string? Message { get; set; }
    public bool IsError { get; set; }
}