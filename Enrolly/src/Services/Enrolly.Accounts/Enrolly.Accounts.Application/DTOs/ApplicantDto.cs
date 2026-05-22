using Enrolly.Accounts.Domain.Entities;
using Enrolly.Accounts.Domain.Enums;

namespace Enrolly.Accounts.Application.DTOs;

public class ApplicantDto
{
    public Guid Id { get; set; }
    public DateOnly DateOfBirth { get; set; } 
    public int? CitizenshipId { get; set; }
    public Gender? Gender { get; set; }
    
    public Citizenship? Citizenship { get; set; }
}