
using Enrolly.EduDictionary.Domain.Enums;

namespace Enrolly.EduDictionary.Domain.Entities;

public class EducationLevel
{
    public EducationLevel () {}
    
    public EducationLevel(int id, string name, RelevanceStatus status = RelevanceStatus.Active)
    {
        Id = id;
        Name = name;
        ImportedAt = DateTime.Now;
        RelevanceStatus = status;
    }

    public int Id  { get; set; }
    public string Name { get; set; } = "";
    public DateTime ImportedAt { get; set; }
    public RelevanceStatus RelevanceStatus { get; set; }
}
