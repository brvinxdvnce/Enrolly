namespace Enrolly.Admissions.Domain.Entities;

public class EducationDocument
{
    public Guid UserId { get; set; }
    public Guid DocumentId { get; set; }
    public Guid DocumentTypeId { get; set; }
}