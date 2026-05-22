using Enrolly.Admissions.Domain.Enums;
using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Admissions.Domain.Entities;

public class Applicant : DomainEntity
{
    public Applicant() {}
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<EducationDocument> Documents { get; set; } = new List<EducationDocument>();
    public List<Manager> Managers { get; set; } = new List<Manager>();

    public List<Admission> Admissions { get; set; } = new List<Admission>();
}