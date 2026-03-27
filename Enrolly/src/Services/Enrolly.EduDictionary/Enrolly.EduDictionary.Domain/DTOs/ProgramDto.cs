using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictoinary.Domain.Entities;

namespace DictionaryWorker.DTOs;

public class ProgramDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Name { get; set; } = "";
    public string Code  { get; set; } = "";
    public string Language { get; set; } = "";
    public string EducationForm { get; set; } = "";
    public Faculty? Faculty { get; set; }
    public EducationLevel? EducationLevel { get; set; }
}