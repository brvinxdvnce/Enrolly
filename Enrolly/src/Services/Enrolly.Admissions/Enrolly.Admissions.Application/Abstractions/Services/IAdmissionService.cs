using Enrolly.Admissions.Application.DTOs;
using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Enums;

namespace Enrolly.Admissions.Application.Abstractions.Services;

public interface IAdmissionService
{
    public Task<Result<IEnumerable<AdmissionViewDto>>> GetAdmissionsByApplicantId(Guid applicantId); 
    public Task<Result<PagedResponce<AdmissionViewDto>>> GetAdmissions(
        string? applicantName,
        string? program,
        string? faculty,
        AdmissionStatus? status,
        bool? isManaged, 
        Guid? managerId,
        OrderDirection? lastUpdateSortDirection,
        int page,
        int pageSize);
    public Task<Result<Guid>> CreateAdmission(Guid applicantId);
    public Task<Result<AdmissionViewDto>> GetAdmission(Guid id);
    public Task<Result> ChangeAdmissionStatus(Guid admissionId, AdmissionStatus status);
    public Task<Result> DeleteAdmission(Guid id);
}