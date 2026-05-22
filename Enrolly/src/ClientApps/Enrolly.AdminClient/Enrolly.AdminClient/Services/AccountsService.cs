using Enrolly.AdminClient.Models;

namespace Enrolly.AdminClient.Services;

public class AccountsService
{
    private readonly HttpClient _http;

    public AccountsService(HttpClient http)
    {
        _http = http;
    }

    public async Task<(LoginResponse? Response, string? Error)> LoginAsync(string email, string password)
    {
        var response = await _http.PostAsJsonAsync("/api/v1/auth/login", new LoginRequest(email, password));

        if (!response.IsSuccessStatusCode)
            return (null, "Неверный логин или пароль");

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        return (result, null);
    }
    
}