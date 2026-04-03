using DictionaryWorker.DTOs;

namespace Enrolly.EduDictionary.Domain.DTOs;

public class DocumentTypeDto
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Name { get; set; } = "";
    public EducationLevelDto? EducationLevel { get; set; } = new EducationLevelDto();
    public ICollection<EducationLevelDto>? NextEducationLevels { get; set; }
}