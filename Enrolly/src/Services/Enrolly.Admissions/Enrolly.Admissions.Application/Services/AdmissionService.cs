using CSharpFunctionalExtensions;
using Enrolly.Admissions.Application.Abstractions.Services;
using Enrolly.Admissions.Application.DTOs;
using Enrolly.Admissions.Application.Mappers;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Admissions.Domain.Enums;
using Enrolly.Admissions.Domain.Repositories;
using Enrolly.Shared.Logging;
using Enrolly.Shared.Logging.Utils.Enums;
using Enrolly.Shared.Logging.Utils.Result;

namespace Enrolly.Admissions.Application.Services;

public class AdmissionService : IAdmissionService
{
    private readonly IAdmissionRepository _admissionRepository;
    private readonly AdmissionMapper _admissionMapper;

    public AdmissionService(IAdmissionRepository admissionRepository, AdmissionMapper admissionMapper)
    {
        _admissionRepository = admissionRepository;
        _admissionMapper = admissionMapper;
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
        return result.Map(pagedResponce =>
            new PagedResponce<AdmissionViewDto>() {
                Content = pagedResponce.Content.Select(_admissionMapper.ToViewDto).ToList(),
                TotalCount = pagedResponce.TotalCount,
                PageNumber = pagedResponce.PageNumber,
                PageSize = pagedResponce.PageSize,
                PagesCount = pagedResponce.PagesCount
            }
        );
    }

    public async Task<Result<Guid>> CreateAdmission(AdmissionCreateDto admission)
    {
        return await _admissionRepository.Add(_admissionMapper.FromCreateDto(admission));
    }

    public async Task<Result<AdmissionViewDto>> GetAdmission(Guid id)
    {
        return await _admissionRepository
            .GetById(id)
            .Map(_admissionMapper.ToViewDto);
    }

    public async Task<Result> ChangeAdmissionStatus(Guid admissionId, AdmissionStatus status)
    {
        return await _admissionRepository.ChangeAdmissionStatus(admissionId, status);
    }

    public async Task<Result> DeleteAdmission(Guid admissionId)
    {
        return await _admissionRepository.DeleteById(admissionId);
    }
}