using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Enums;

namespace Enrolly.Admissions.Application.DTOs;

public class AdmissionViewDto
{
    public Guid Id { get; set; }
    public Guid ApplicantId { get; set; }
    public Guid? ManagerId { get; set; }
    public AdmissionStatus AdmissionStatus { get; set; }
    public List<AdmissionProgram> Programs { get; set; } = new List<AdmissionProgram>();
    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;
    public DateTime LastUpdateTime { get; set; }
}