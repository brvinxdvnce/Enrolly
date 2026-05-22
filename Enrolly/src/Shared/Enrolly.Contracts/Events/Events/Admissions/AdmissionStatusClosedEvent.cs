using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events.Admissions;

public record AdmissionStatusClosedEvent(
    Guid ApplicantId,
    Guid AdmissionId,
    string ApplicantEmail,
    string ApplicantName
) : IEvent;