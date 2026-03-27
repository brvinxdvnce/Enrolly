namespace Enrolly.Accounts.Domain.Entities;

public class Manager
{
    public Guid Id { get; private set; }
    public Guid? FacultyId { get; set; }
    
    public User Account { get; set; }
}