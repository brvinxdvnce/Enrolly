using Enrolly.Admissions.Domain.Entities;
using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Enums;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IAdmissionRepository
{
    public Task<Result<ICollection<Admission>>> GetByApplicantId(Guid userId); 
    public Task<Result<PagedResponce<Admission>>> GetMany(
        string? applicantName,
        string? program,
        string? faculty,
        AdmissionStatus? status,
        bool? isManaged, 
        Guid? managerId,
        OrderDirection? lastUpdateSortDirection,
        int page,
        int pageSize);
    public Task<Result<Guid>> Add(Admission admission);
    public Task<Result<Admission>> GetById(Guid admissionId);
    public Task<Result> DeleteById(Guid admissionId);
    public Task<Result> AppointManager(Guid admissionId, Guid managerId);
    public Task<Result> DismissManager(Guid admissionId);
    public Task<Result<Applicant>> GetApplicant(Guid admissionId);
    public Task<Result> ChangeAdmissionStatus(Guid admissionId, AdmissionStatus status);
}