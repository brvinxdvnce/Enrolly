namespace Enrolly.Documents.Application.DTOs;

public class FileDto
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string OriginalName { get; set; }
    public string? Url { get; set; }
    public string? Extension { get; set; }
    public string ContentType { get; set; }
    public Guid? EducationDocumentId { get; set; }
    public Guid? PassportId { get; set; }
    
}