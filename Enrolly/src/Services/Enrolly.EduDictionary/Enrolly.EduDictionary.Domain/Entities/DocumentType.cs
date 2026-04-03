using Enrolly.EduDictionary.Domain.Enums;

namespace Enrolly.EduDictionary.Domain.Entities;

public class DocumentType
{
    public DocumentType() {}

    public DocumentType(
        Guid id,
        string name, 
        DateTime createdAt,
        int educationLevelId,
        RelevanceStatus status = RelevanceStatus.Active
        )
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        ImportedAt = DateTime.UtcNow;
        EducationLevelId = educationLevelId;
        NextEducationLevels = new List<EducationLevel>();
        RelevanceStatus = status;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime ImportedAt { get; set; }
    public RelevanceStatus RelevanceStatus { get; set; }
    
    public int EducationLevelId { get; set; }
    public EducationLevel? EducationLevel { get; set; }
    public ICollection<EducationLevel?> NextEducationLevels { get; set; }
}