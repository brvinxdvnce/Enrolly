using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Application.Mappers;
using Enrolly.Accounts.Application.Services.Interfaces;
using Enrolly.Accounts.Domain.Repositories;
using Enrolly.Contracts.Events;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Enrolly.Accounts.Application.Services.Implementations;

public class ApplicantService : IApplicantService
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly ILogger<ApplicantService> _logger;
    private readonly ApplicantMapper _mapper;
    

    public ApplicantService(IApplicantRepository applicantRepository, ApplicantMapper mapper, ILogger<ApplicantService> logger)
    {
        _applicantRepository = applicantRepository;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<Guid> CreateApplicantAsync(Guid id, ApplicantDto dto)
    {
        var applicant = _mapper.FromDto(dto);
        await _applicantRepository.CreateApplicantAsync(id, applicant);
        return id;
    }

    public async Task<ApplicantDto> GetApplicantByIdAsync(Guid id)
    {
        var applicant = await _applicantRepository.GetApplicantByIdAsync(id);
        return _mapper.ToDto(applicant);
    }

    public async Task<IEnumerable<ApplicantDto>> GetApplicantsAsync()
    {
        var applicants = await _applicantRepository.GetApplicantsAsync();
        return _mapper.ToDtos(applicants);
    }

    public async Task UpdateApplicantAsync(Guid id, ApplicantDto dto)
    {
        var applicant = _mapper.FromDto(dto);
        await _applicantRepository.UpdateApplicantAsync(id, applicant);
    }

    public async Task DeleteApplicantAsync(Guid id)
    {
        await _applicantRepository.DeleteApplicantAsync(id);
    }
}