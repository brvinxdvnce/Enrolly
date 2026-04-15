using Enrolly.Admissions.Domain.Enums;

namespace Enrolly.Admissions.Domain.Entities;

public class Admission
{
    public Admission () {}
    
    public Guid Id { get; private set; }
    public Guid UserId { get; set; }
    public Guid? ManagerId { get; set; }
    public AdmissionStatus AdmissionStatus { get; set; }
    public List<AdmissionProgram>? Programs { get; set; }
    
    public User? User { get; set; }
    public User? Manager { get; set; }
}