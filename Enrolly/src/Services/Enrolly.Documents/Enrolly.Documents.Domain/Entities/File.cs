
namespace Enrolly.Documents.Domain.Entities;

public class File
{
    public File () {}

    public File(Guid userId, string fileName, string extension)
    {
        UserId = userId;
        Id = Guid.NewGuid();
        PassportId = userId;
        EducationDocumentId = null;
        OriginalName = fileName;
        Extension = extension;
    }
    
    public File(Guid userId, Guid documentId, string fileName, string extension)
    {
        UserId = userId;
        Id = Guid.NewGuid();
        EducationDocumentId = documentId;
        PassportId = null;
        OriginalName = fileName;
        Extension = extension;
    }

    public Guid UserId { get; set; }
    public Guid Id { get; set; }
    public string OriginalName { get; set; }
    public string? Url { get; set; }
    public string? Extension { get; set; }
    public string ContentType { get; set; }
    public Guid? EducationDocumentId { get; set; }
    public Guid? PassportId { get; set; }
    
    public EducationDocument? EducationDocument { get; set; }
    public Passport? Passport { get; set; }
}