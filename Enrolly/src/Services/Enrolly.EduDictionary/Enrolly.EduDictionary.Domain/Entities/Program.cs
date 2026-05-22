using Enrolly.Contracts.Events.Abstractions;
using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictionary.Domain.Enums;

namespace Enrolly.EduDictoinary.Domain.Entities;

public class Program : DomainEntity
{
    public Program () {}
    
    public Program(
        Guid id,
        string name,
        string code,
        string language,
        string educationForm,
        DateTime createdAt,
        Guid facultyId,
        int educationLevelId,
        RelevanceStatus status = RelevanceStatus.Active
        )
    {
        Id = id;
        CreatedAt =  createdAt;
        ImportedAt = DateTime.UtcNow;
        RelevanceStatus = status;
        Name = name;
        Code = code;
        Language = language;
        EducationForm = educationForm;
        FacultyId = facultyId;
        EducationLevelId = educationLevelId;
    }
    
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ImportedAt { get; set; }
    public RelevanceStatus RelevanceStatus { get; set; }
    public string Name { get; set; } = "";
    public string Code  { get; set; } = "";
    public string Language { get; set; } = "";
    public string EducationForm { get; set; } = "";
    public Guid FacultyId { get; set; }
    public int EducationLevelId { get; set; }
    public Faculty? Faculty { get; set; }
    public EducationLevel? EducationLevel { get; set; }
}