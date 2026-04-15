using Enrolly.Contracts.Events.Abstractions;

namespace Enrolly.Contracts.Events.Events;

public record ManagerAssignedToApplicationEvent(
    Guid applicantId,
    Guid managerId,
    string applicantEmail,
    string managerEmail) : IEvent;