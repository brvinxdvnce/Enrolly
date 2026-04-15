namespace Enrolly.Accounts.Application.DTOs;

public class UserViewDto
{
    public virtual Guid Id { get; set; } = default!;
    public virtual string? UserName { get; set; }
    public virtual string? Email { get; set; }
    public virtual bool EmailConfirmed { get; set; }
    public virtual string? PhoneNumber { get; set; }
}