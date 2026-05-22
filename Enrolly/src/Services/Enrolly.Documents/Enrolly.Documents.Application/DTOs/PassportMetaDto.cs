using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Application.DTOs;

public class PassportMetaDto
{
    public Guid Id { get; set; }
    public string Fullname  { get; set; }
    public string DepartmentCode { get; set; }
    public string Registration { get; set; }
    public string Series { get; set; }
    public string Number { get; set; }
    public DateOnly IssueDate { get; set; }
    public string IssuedBy { get; set; }
    public ICollection<FileDto> Files { get; set; }
}