using Enrolly.AdminClient.Models;
using Enrolly.AdminClient.Models.Models;

namespace Enrolly.AdminClient.Services;

public class ImportsService
{
    private readonly HttpClient _httpClient;
    
    public ImportsService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ImportSummary?> GetLastImportAsync()
    {
        return await _httpClient.GetFromJsonAsync<ImportSummary>("/api/v1/dictionary/imports");
    }

    public async Task<List<ImportSummary>> GetImportHistoryAsync(DateTime? from, DateTime? to)
    {
        var query = new List<string>();
        if (from.HasValue) query.Add($"from={from.Value:O}");
        if (to.HasValue)   query.Add($"to={to.Value:O}");
        var qs = query.Count > 0 ? "?" + string.Join("&", query) : "";
 
        return await _httpClient.GetFromJsonAsync<List<ImportSummary>>(
                   $"/api/v1/dictionary/imports/history{qs}")
               ?? [];
    }
    
    public async Task<(ImportSummary? Result, string? Error)> StartImportAsync(string importType)
    {
        try
        {
            var response = await _httpClient.PostAsync(
                $"/api/v1/dictionary/imports?import={Uri.EscapeDataString(importType)}", null);
 
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return (null, $"Ошибка: {body}");
            }
 
            var result = await response.Content.ReadFromJsonAsync<ImportSummary>();
            return (result, null);
        }
        catch (Exception ex)
        {
            return (null, $"Ошибка соединения: {ex.Message}");
        }
    }
}