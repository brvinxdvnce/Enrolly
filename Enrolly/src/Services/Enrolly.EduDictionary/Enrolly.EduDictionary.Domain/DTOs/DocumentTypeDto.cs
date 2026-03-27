using Enrolly.EduDictionary.Domain.Entities;
using Enrolly.EduDictoinary.Domain.Entities;

namespace DictionaryWorker.DTOs;

public class DocumentTypeDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ImportedAt { get; set; }
    public string Name { get; set; } = "";
    public EducationLevel? EducationLevel { get; set; } = new EducationLevel();
    public EducationLevel? NextEducationLevel { get; set; } =  new EducationLevel();
}