namespace Enrolly.Admissions.Domain.Entities;

public class Document
{
    public Document() {}
    
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public DateTime CreateTime { get; set; }
    public string Name { get; set; } = "";
    public EducationLevel? EducationLevel { get; set; } = new EducationLevel();
    public ICollection<EducationLevel>? NextEducationLevels { get; set; }
}