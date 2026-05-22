using Enrolly.Accounts.Domain.Enums;
using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Accounts.Domain.Entities;

public class Manager : DomainEntity
{
    public Manager () {}
    
    public Guid Id { get; set; }
    public Guid? FacultyId { get; set; }
    public ManagerGrade Grade { get; set; }
    
    public User Account { get; set; }
    public List<Applicant> PendingApplicants { get; set; } = new List<Applicant>();
}