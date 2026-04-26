namespace Enrolly.Admissions.Domain.Entities;

public class AdmissionProgram
{
    public AdmissionProgram () {}

    public AdmissionProgram(Guid admissionId, Guid programId, int programPriority = 1)
    {
        AdmissionId = admissionId;
        ProgramId = programId;
        Priority = programPriority;
    }
    
    public Guid ProgramId { get; set; }
    public Guid AdmissionId { get; set; }
    public int Priority { get; set; }
    
    public Program Program { get; set; }
    
}