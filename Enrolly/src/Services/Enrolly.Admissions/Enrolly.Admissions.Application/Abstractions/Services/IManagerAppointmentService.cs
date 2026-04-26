using CSharpFunctionalExtensions;

namespace Enrolly.Admissions.Application.Abstractions.Services;

public interface IManagerAppointmentService
{
    public Task<Result> AppointManager(Guid admissionId, Guid managerId);
    public Task<Result> DismissManager(Guid admissionId);
}