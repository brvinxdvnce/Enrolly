using Enrolly.Accounts.Domain.Enums;

namespace Enrolly.Accounts.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Fullname { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public Gender? Gender { get; set; }
    public Guid? CitizenshipId { get; set; }
    public Role Role  { get; set; }
}


public class UserUpdateDto
{
    public string Fullname { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public Guid CitizenshipId { get; set; }
}















public class UserExample
{
    public Guid Id { get; private set; }
    public string Fullname { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
}

public class StudentExample
{
    public Guid Id { get; private set; }
    public DateTime? DateOfBirth { get; set; }
    public Gender? Gender { get; set; }
    public Guid? CitizenshipId { get; set; }
    public Role Role  { get; set; }
}

public class ManagerExample
{
    public Guid Id { get; private set; }
    public Role Role  { get; set; }
    public Guid? FacultyId { get; set; }
}