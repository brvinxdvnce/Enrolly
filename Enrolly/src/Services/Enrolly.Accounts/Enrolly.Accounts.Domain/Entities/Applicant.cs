using Enrolly.Accounts.Domain.Enums;
using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Accounts.Domain.Entities;

public class Applicant : DomainEntity
{
    public Applicant () {}
    
    
    public Guid Id { get; set; }
    public DateOnly DateOfBirth { get; set; } 
    public Guid? CitizenshipId { get; set; }
    public Gender? Gender { get; set; }
    
    public Citizenship? Citizenship { get; set; }
    public User Account { get; set; }
}