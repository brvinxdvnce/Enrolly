using Enrolly.Shared.Logging.Utils.Models;

namespace Enrolly.Documents.Domain.Entities;

public class Manager
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public ManagerGrade Grade { get; set; }

    public List<Applicant> PendingApplicants { get; set; } = new List<Applicant>();
}