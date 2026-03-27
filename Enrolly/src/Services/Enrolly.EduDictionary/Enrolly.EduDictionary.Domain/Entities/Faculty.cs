using Enrolly.EduDictionary.Domain.Enums;

namespace Enrolly.EduDictoinary.Domain.Entities;

public class Faculty
{
    private Faculty () {}

    public Faculty(
        Guid id,
        string name,
        DateTime createdAt,
        RelevanceStatus status = RelevanceStatus.Active)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        RelevanceStatus = status;
    }
    
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ImportedAt { get; set; }
    public RelevanceStatus RelevanceStatus { get; set; }
    public string Name { get; set; } = "";
}