namespace Enrolly.Accounts.Presentation.DTOs;

public class ChangeEmailRequestDto
{
    public string OldEmail { get; set; }
    public string NewEmail { get; set; }
    public string Password { get; set; }
}