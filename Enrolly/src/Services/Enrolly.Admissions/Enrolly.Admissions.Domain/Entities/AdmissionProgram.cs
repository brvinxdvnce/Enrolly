namespace Enrolly.Admissions.Domain.Entities;

public class AdmissionProgram
{
    public Guid Id { get; private set; }
    public Guid ProgramId { get; set; }
    public Guid AdmissionId { get; set; }
}