namespace Enrolly.Admissions.Domain.Entities;

public class AdmissionProgram
{
    public AdmissionProgram () {}
    
    public Guid Id { get; private set; }
    public Guid ProgramId { get; set; }
    public Guid AdmissionId { get; set; }
    public int Priority { get; set; }
}