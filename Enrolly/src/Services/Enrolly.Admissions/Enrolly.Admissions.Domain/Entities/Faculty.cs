namespace Enrolly.Admissions.Domain.Entities;

public class Faculty
{
    public Guid Id { get; set; }
    public DateTime CreateTime { get; set; }
    public string Name { get; set; } = "";
}