namespace Enrolly.Admissions.Domain.Entities;

public class EducationDocumentType
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Name { get; set; } = "";
    public int EducationLevelId { get; set; }
    public ICollection<int> NextEducationLevelIds { get; set; } = new List<int>();
    public EducationLevel? EducationLevel { get; set; }
    public ICollection<EducationLevel>? NextEducationLevels { get; set; } = new List<EducationLevel>();
}