using CSharpFunctionalExtensions;
using Enrolly.Admissions.Application.Abstractions.Services;
using Enrolly.Admissions.Application.DTOs;
using Enrolly.Admissions.Application.Mappers;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Contracts.Events.Events.Admissions;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Enums;
using Enrolly.Shared.Logging.Utils.Result;

namespace Enrolly.Admissions.Application.Services;

public class AdmissionService : IAdmissionService
{
    private readonly IAdmissionRepository _admissionRepository;
    private readonly IApplicantRepository _applicantRepository;
    private readonly AdmissionMapper _admissionMapper;

    public AdmissionService(IAdmissionRepository admissionRepository, AdmissionMapper admissionMapper, IApplicantRepository applicantRepository)
    {
        _admissionRepository = admissionRepository;
        _applicantRepository = applicantRepository;
        _admissionMapper = admissionMapper;
    }
    
    public async Task<Result<IEnumerable<AdmissionViewDto>>> GetAdmissionsByApplicantId(Guid applicantId)
    {
        return await _admissionRepository.GetByApplicantId(applicantId)
            .Map(admissions =>
                admissions.Select(a => _admissionMapper.ToViewDto(a)));
    }

    public async Task<Result<PagedResponce<AdmissionViewDto>>> GetAdmissions(
        string? applicantName,
        string? program,
        string? faculty,
        AdmissionStatus? status,
        bool? isManaged,
        Guid? managerId,
        OrderDirection? lastUpdateSortDirection,
        int page,
        int pageSize)
    {
        var result = await _admissionRepository.GetMany(applicantName, program, faculty, status, isManaged, managerId, lastUpdateSortDirection, page, pageSize); 
        return result.Map(pagedResponse =>
            new PagedResponce<AdmissionViewDto>() {
                Content = pagedResponse.Content.Select(_admissionMapper.ToViewDto).ToList(),
                TotalCount = pagedResponse.TotalCount,
                PageNumber = pagedResponse.PageNumber,
                PageSize = pagedResponse.PageSize,
                PagesCount = pagedResponse.PagesCount
            }
        );
    }

    public async Task<Result<Guid>> CreateAdmission(Guid applicantId)
    {
        var admission = new Admission(applicantId);
        var applicant = (await _applicantRepository.GetById(applicantId)).Value;
        
        var currentAdmissions = await _admissionRepository.GetByApplicantId(applicantId);
        
        if (currentAdmissions.Value.Count == 0)
            admission.AddEvent(new AdmissionStatusOpenedEvent(
                applicantId, admission.Id, applicant.Email, applicant.Name));
            
        return await _admissionRepository.Add(admission);
    }

    public async Task<Result<AdmissionViewDto>> GetAdmission(Guid id)
    {
        return await _admissionRepository
            .GetById(id)
            .Map(_admissionMapper.ToViewDto);
    }

    public async Task<Result> ChangeAdmissionStatus(Guid admissionId, AdmissionStatus status)
    {
        //if (status != AdmissionStatus.Closed) 
        return await _admissionRepository.ChangeAdmissionStatus(admissionId, status);

        /*return await _admissionRepository.GetById(admissionId)
            .Bind(async admission => await _applicantRepository.GetById(admission.ApplicantId)
                .Map(applicant => (admission, applicant)))
            .Tap(pair =>
            {
                pair.applicant.AddEvent(new AdmissionStatusClosedEvent(
                    pair.admission.ApplicantId,
                    pair.admission.Id,
                    pair.applicant.Email,
                    pair.applicant.Name
                ));
            })
            .Bind(async pair => await _applicantRepository.Update(pair.applicant))
            .Bind(async _ => await _admissionRepository.ChangeAdmissionStatus(admissionId, status));*/
    }

    public async Task<Result> DeleteAdmission(Guid admissionId)
    {
        return await _admissionRepository.DeleteById(admissionId);
    }
}