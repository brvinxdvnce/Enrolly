namespace Enrolly.Accounts.Presentation.DTOs;

public class LoginRequestDto
{
    public string Email { get; init; }

    public required string Password { get; init; } 
}