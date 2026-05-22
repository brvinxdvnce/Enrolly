using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Dictionary.ProgramEvents;

public record ProgramUpdatedEvent(
    Guid Id,
    DateTime CreatedAt,
    string Name,
    string Code,
    string Language,
    string EducationForm,
    Guid? FacultyId,
    int? EducationLevelId
) : IEvent;