namespace Enrolly.Accounts.Presentation.DTOs;

public class ChangePasswordRequestDto
{
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
}