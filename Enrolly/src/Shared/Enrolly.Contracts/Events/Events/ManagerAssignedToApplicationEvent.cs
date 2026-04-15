using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events;

public record ManagerAssignedToApplicationEvent(
    Guid ApplicantId,
    Guid ManagerId,
    string ApplicantEmail,
    string ManagerEmail,
    string ApplicantName,
    string ManagerName) : IEvent;