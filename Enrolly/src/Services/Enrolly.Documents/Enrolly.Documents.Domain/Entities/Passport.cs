namespace Enrolly.Documents.Domain.Entities;

public class Passport
{
    public Passport () {}
    
    public Guid Id { get; set; }
    public string Fullname  { get; set; }
    public string DepartmentCode { get; set; }
    public string Registration { get; set; }
    public string Series { get; set; }
    public string Number { get; set; }
    public DateOnly IssueDate { get; set; }
    public string IssuedBy { get; set; }
    
    public ICollection<File> Files { get; set; }
    
    public Applicant Applicant { get; set; }
}