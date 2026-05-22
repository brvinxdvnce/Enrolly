using CSharpFunctionalExtensions;
using Enrolly.Documents.Application.Abstractions.ServicesV2;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Domain.Repositories;

namespace Enrolly.Documents.Application.ServicesV2;

public class PassportMetaServiceV2 : IPassportMetaServiceV2
{
    private readonly IApplicantRepository _applicantRepository;
    private readonly IPassportRepositoryV2 _passportRepository;
    private readonly PassportMapper _mapper;
    
    public PassportMetaServiceV2(IApplicantRepository applicantRepository, PassportMapper mapper, IPassportRepositoryV2 passportRepository)
    {
        _applicantRepository = applicantRepository;
        _mapper = mapper;
        _passportRepository = passportRepository;
    }

    public async Task<Result<Guid>> CreatePassportMeta(Guid applicantId, PassportMetaDto dto)
    {
        return await Result.Success()
            .Ensure(async () => await _applicantRepository.IsActive(applicantId))
            .Tap(() => dto.Id = applicantId)
            .Bind(async () => await _passportRepository.CreateAsync(_mapper.FromDto(dto)));
    }

    public async Task<Result<PassportMetaDto>> GetPassportMeta(Guid applicantId)
    {
        return await Result.Success()
            .Bind(async () => await _passportRepository.GetByIdAsync(applicantId))
            .Map(passportMeta => _mapper.ToDto(passportMeta));
    }

    public async Task<Result> UpdatePassportMeta(Guid applicantId, PassportMetaDto dto)
    {
        return await Result.Success()
            .Ensure(async () => await _applicantRepository.IsActive(applicantId))
            .Tap(() => dto.Id = applicantId)
            .Bind(async () => await _passportRepository.UpdateAsync(_mapper.FromDto(dto)));
    }

    public async Task<Result> DeletePassportMeta(Guid applicantId)
    {
        return await Result.Success()
            .Ensure(async () => await _applicantRepository.IsActive(applicantId))
            .Bind(async () => await _passportRepository.DeleteAsync(applicantId));
    }
}