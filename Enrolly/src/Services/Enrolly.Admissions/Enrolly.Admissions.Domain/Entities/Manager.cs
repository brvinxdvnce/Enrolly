using Enrolly.Admissions.Domain.Enums;

namespace Enrolly.Admissions.Domain.Entities;

public class Manager
{
    public Manager () {}
    
    public Guid Id { get; set; }
    public Guid? FacultyId { get; set; }
    public ManagerGrade Grade { get; set; }
    
    public ICollection<Admission> LeadingAdmissions { get; set; }
}