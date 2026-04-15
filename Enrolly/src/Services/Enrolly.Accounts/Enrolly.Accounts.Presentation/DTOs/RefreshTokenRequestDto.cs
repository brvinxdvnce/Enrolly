namespace Enrolly.Accounts.Presentation.DTOs;

public class RefreshTokenRequestDto
{
    public required string AccessToken {get; init;}
    public required string RefreshToken {get; init;}
}