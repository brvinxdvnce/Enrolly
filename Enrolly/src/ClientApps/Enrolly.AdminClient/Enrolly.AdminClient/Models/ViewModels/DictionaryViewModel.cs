namespace Enrolly.AdminClient.Models;

public class DictionaryViewModel
{
    public List<Faculty> Faculties { get; set; } = [];
    public List<EducationLevel> EducationLevels { get; set; } = [];
    public List<DocumentType> DocumentTypes { get; set; } = [];
}