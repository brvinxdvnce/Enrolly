using System.Reflection.Metadata;
using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Documents.Domain.Entities;

public class EducationDocument : DomainEntity
{
    public EducationDocument () {}
    
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Series { get; set; }
    public string Number { get; set; }
    public DateOnly IssueDate { get; set; }
    public string IssuedBy { get; set; }
    public string? Qualification { get; set; }
    public string Speciality { get; set; }
    public double? AverageGrade { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid DocumentTypeId { get; set; }
    
    public EducationDocumentType DocumentType { get; set; }
    public ICollection<File> Files { get; set; }
    public Applicant Applicant { get; set; }
}