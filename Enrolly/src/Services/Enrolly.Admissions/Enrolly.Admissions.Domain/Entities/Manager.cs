using Enrolly.Admissions.Domain.Enums;
using Enrolly.Shared.Logging.Utils.Models;

namespace Enrolly.Admissions.Domain.Entities;

public class Manager
{
    public Manager () {}
    
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid? FacultyId { get; set; }
    public ManagerGrade Grade { get; set; } = ManagerGrade.DefaultManager;
    
    public ICollection<Admission> PendingAdmissions { get; set; } = new List<Admission>();
    
}