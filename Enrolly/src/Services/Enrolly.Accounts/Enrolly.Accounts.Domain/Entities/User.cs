using Enrolly.Accounts.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Enrolly.Accounts.Domain.Entities;

public class User : IdentityUser<Guid>
{
    public Applicant? ApplicantProfile  { get; set; }
    public Manager? ManagerProfile { get; set; }
}