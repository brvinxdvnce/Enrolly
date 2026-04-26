using Enrolly.Admissions.Application.Abstractions.Services;
using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Shared.Logging.Utils.Result;

namespace Enrolly.Admissions.Application.Services;

public class ManagerAppointmentService : IManagerAppointmentService
{
    private readonly IAdmissionRepository _admissionRepository;
    private readonly IApplicantRepository _applicantRepository;
    
    public ManagerAppointmentService(IAdmissionRepository admissionRepository, IApplicantRepository applicantRepository)
    {
        _admissionRepository = admissionRepository;
        _applicantRepository = applicantRepository;
    }

    public async Task<Result> AppointManager(Guid admissionId, Guid managerId)
    {
        return await _admissionRepository.AppointManager(admissionId, managerId)
            .Bind(async () => await _admissionRepository.GetApplicant(admissionId))
            .Bind(async applicant => await _applicantRepository.AddManagerById(applicant.Id, managerId));
    }

    public async Task<Result> DismissManager(Guid admissionId)
    {
        return await _admissionRepository.GetById(admissionId)
            .Ensure(admission => admission.ManagerId is not null,
                ResultError.Ok("The admission does not have an assigned manager"))
            .Bind(async admission =>
            {
                Guid? managerId = admission.ManagerId;
                
                return await _applicantRepository.GetById(admission.ApplicantId)
                    .Tap(async _ => await _admissionRepository.DismissManager(admissionId))
                    .Bind(async applicant => await _applicantRepository.DeleteManagerById(applicant.Id, (Guid)managerId!));
            });
    }
}
