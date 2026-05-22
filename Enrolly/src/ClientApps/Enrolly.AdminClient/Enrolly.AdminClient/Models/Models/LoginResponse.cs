namespace Enrolly.AdminClient.Models;

public record LoginResponse(string UserId, string AccessToken, string RefreshToken);