using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Enums;

namespace Enrolly.EduDictoinary.Domain.Entities;

public class DocumentType
{
    private DocumentType() {}

    public DocumentType(
        Guid id,
        string name, 
        DateTime createdAt,
        int educationLevelId,
        int nexteducationLevelId,
        RelevanceStatus status = RelevanceStatus.Active
        )
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        ImportedAt = DateTime.Now;
        EducationLevelId = educationLevelId;
        NextEducationLevelId = nexteducationLevelId;
        RelevanceStatus = status;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime ImportedAt { get; set; }
    public RelevanceStatus RelevanceStatus { get; set; }
    
    public int EducationLevelId { get; set; }
    public int NextEducationLevelId { get; set; }
    public EducationLevel? EducationLevel { get; set; }
    public EducationLevel? NextEducationLevel { get; set; }
}