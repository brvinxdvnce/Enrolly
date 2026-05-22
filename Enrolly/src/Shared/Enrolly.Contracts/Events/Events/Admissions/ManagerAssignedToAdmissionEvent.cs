using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events;

public record ManagerAssignedToAdmissionEvent(
    Guid ApplicantId,
    Guid ManagerId,
    Guid AdmissionId,
    string ApplicantEmail,
    string ManagerEmail,
    string ApplicantName,
    string ManagerName) : IEvent;