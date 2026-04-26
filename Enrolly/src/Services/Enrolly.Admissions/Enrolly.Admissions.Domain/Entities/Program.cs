namespace Enrolly.Admissions.Domain.Entities;

public class Program  
{
    public Program () {}
    
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Name { get; set; } = "";
    public string Code  { get; set; } = "";
    public string Language { get; set; } = "";
    public string EducationForm { get; set; } = "";
    public Faculty? Faculty { get; set; }
    public EducationLevel? EducationLevel { get; set; }
}