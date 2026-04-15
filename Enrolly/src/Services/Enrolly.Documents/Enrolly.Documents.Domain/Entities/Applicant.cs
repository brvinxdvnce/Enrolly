namespace Enrolly.Documents.Domain.Entities;

public class Applicant
{
    public Applicant() {}
    
    public Guid Id { get; set; }
    public string Email { get; set; }
    public bool IsAdmissionActive { get; set; } = false;
    public Guid? ManagerId { get; set; }
    public Passport? Passport { get; set; }
    public ICollection<EducationDocument>? Diplomas { get; set; }
}