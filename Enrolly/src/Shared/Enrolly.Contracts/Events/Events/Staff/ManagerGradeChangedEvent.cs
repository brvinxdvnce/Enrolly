using Enrolly.Contracts.Events.Abstractions;
using Enrolly.Shared.Logging.Utils.Models;

namespace Enrolly.Contracts.Events.Events.Staff;

public record ManagerGradeChangedEvent (Guid Id, ManagerGrade NewGrade) : IEvent;