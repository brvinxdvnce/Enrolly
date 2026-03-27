using Enrolly.Accounts.Domain.Enums;

namespace Enrolly.Accounts.Domain.Entities;

public class Applicant
{
    public Guid Id { get; private set; }
    public DateOnly DateOfBirth { get; set; } 
    public Guid? CitizenshipId { get; set; }
    public Gender? Gender { get; set; }
    
    public Citizenship? Citizenship { get; set; }
    public User Account { get; set; }
}