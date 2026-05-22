using Enrolly.Admissions.Domain.Enums;
using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Admissions.Domain.Entities;

public class Admission : DomainEntity
{
    public Admission () {}

    public Admission(Guid applicantId)
    {
        Id = Guid.NewGuid();
        ApplicantId = applicantId;
        AdmissionStatus = AdmissionStatus.Created;
        LastUpdateTime = DateTime.UtcNow;
    }
    
    public Guid Id { get; private set; }
    public Guid ApplicantId { get; set; }
    public Guid? ManagerId { get; set; }
    public AdmissionStatus AdmissionStatus { get; set; }
    public List<AdmissionProgram> Programs { get; set; } = new List<AdmissionProgram>();
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public DateTime LastUpdateTime { get; set; }
    
    public Applicant? Applicant { get; set; }
}