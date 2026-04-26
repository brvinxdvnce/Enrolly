using Enrolly.Admissions.Domain.Enums;

namespace Enrolly.Admissions.Domain.Entities;

public class Applicant
{
    public Applicant() {}
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Document> Documents { get; set; }
    public List<Guid> Managers { get; set; }
    
    public List<Admission> Admissions { get; set; }
}