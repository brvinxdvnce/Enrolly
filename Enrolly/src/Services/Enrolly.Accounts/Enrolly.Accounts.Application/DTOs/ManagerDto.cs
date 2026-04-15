using Enrolly.Accounts.Domain.Enums;

namespace Enrolly.Accounts.Application.DTOs;

public class ManagerDto
{
    public Guid Id { get; set; }
    public ManagerGrade Grade { get; set; }
    public Guid? FacultyId { get; set; }
}