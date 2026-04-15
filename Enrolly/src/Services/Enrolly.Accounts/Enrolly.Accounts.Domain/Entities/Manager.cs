using Enrolly.Accounts.Domain.Enums;

namespace Enrolly.Accounts.Domain.Entities;

public class Manager
{
    public Manager () {}
    
    public Guid Id { get; set; }
    public Guid? FacultyId { get; set; }
    public ManagerGrade Grade { get; set; }
    
    public User Account { get; set; }
}