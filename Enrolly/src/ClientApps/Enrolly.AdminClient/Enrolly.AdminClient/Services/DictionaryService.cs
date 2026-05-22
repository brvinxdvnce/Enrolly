using Enrolly.AdminClient.Models;
using Enrolly.Shared.Logging;

namespace Enrolly.AdminClient.Services;

public class DictionaryService
{
    private readonly HttpClient _http;

    public DictionaryService(HttpClient http)
    {
        _http = http;
    }
    
    public async Task<List<Faculty>> GetFacultiesAsync() =>
        await _http.GetFromJsonAsync<List<Faculty>>("/api/v1/dictionary/faculties") ?? [];
    
    public async Task<List<EducationLevel>> GetEducationLevelsAsync() =>
        await _http.GetFromJsonAsync<List<EducationLevel>>("/api/v1/dictionary/edulevels") ?? [];
    
    public async Task<List<DocumentType>> GetDocumentTypesAsync() =>
        await _http.GetFromJsonAsync<List<DocumentType>>("/api/v1/dictionary/doctypes") ?? [];
    
    public async Task<PagedResponce<Enrolly.AdminClient.Models.Models.Program>?> GetProgramsAsync(
        Guid? facultyId = null,
        int? educationLevelId = null,
        string? educationForm = null,
        string? language = null,
        string? programName = null,
        string? programCode = null,
        int page = 1,
        int pageSize = 10)
    {
        var parts = new List<string>();
        if (facultyId.HasValue)       parts.Add($"facultyId={facultyId}");
        if (educationLevelId.HasValue) parts.Add($"educationLevelId={educationLevelId}");
        if (!string.IsNullOrEmpty(educationForm)) parts.Add($"educationForm={Uri.EscapeDataString(educationForm)}");
        if (!string.IsNullOrEmpty(language))      parts.Add($"language={Uri.EscapeDataString(language)}");
        if (!string.IsNullOrEmpty(programName))   parts.Add($"programName={Uri.EscapeDataString(programName)}");
        if (!string.IsNullOrEmpty(programCode))   parts.Add($"programCode={Uri.EscapeDataString(programCode)}");
        parts.Add($"page={page}");
        parts.Add($"pageSize={pageSize}");
        var query = "?" + string.Join("&", parts);
 
        return await _http.GetFromJsonAsync<PagedResponce<Enrolly.AdminClient.Models.Models.Program>>(
            $"/api/v1/dictionary/programs{query}");
    }
}