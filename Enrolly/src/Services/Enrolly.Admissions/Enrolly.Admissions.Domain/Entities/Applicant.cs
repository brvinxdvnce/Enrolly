using Enrolly.Admissions.Domain.Enums;
using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Admissions.Domain.Entities;

public class Applicant : DomainEntity
{
    public Applicant() {}
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Document> Documents { get; set; }
    public List<Manager> Managers { get; set; }
    
    public List<Admission> Admissions { get; set; }
}