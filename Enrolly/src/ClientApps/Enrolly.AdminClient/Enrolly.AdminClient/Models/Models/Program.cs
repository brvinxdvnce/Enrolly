namespace Enrolly.AdminClient.Models.Models;

public record Program(
    Guid Id,
    string Name,
    string Code,
    string? EducationForm,
    string? Language,
    Faculty? Faculty,
    EducationLevel? EducationLevel
);