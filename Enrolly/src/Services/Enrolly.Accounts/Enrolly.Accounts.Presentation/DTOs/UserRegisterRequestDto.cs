namespace Enrolly.Accounts.Presentation.DTOs;

public class UserRegisterRequestDto
{ 
    public virtual string? UserName { get; set; }

    public virtual string? Email { get; set; }

    public virtual string? Password { get; set; }
    
    public virtual string? PhoneNumber { get; set; }
}
