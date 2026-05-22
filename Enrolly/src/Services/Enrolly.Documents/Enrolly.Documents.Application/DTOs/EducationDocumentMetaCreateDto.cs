using Enrolly.Documents.Domain.Entities;
using File = Enrolly.Documents.Domain.Entities.File;

namespace Enrolly.Documents.Application.DTOs;

public class EducationDocumentMetaCreateDto
{
    public string Series { get; set; }
    public string Number { get; set; }
    public DateOnly IssueDate { get; set; }
    public string IssuedBy { get; set; }
    public string? Qualification { get; set; }
    public string Speciality { get; set; }
    public double? AverageGrade { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid DocumentTypeId { get; set; }
}