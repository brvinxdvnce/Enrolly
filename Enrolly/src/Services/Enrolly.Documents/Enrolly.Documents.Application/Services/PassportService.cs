using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;

namespace Enrolly.Documents.Application.Services;

public class PassportService : IPassportService
{
    private readonly IPassportRepository _passportRepository;
    private readonly PassportMapper _mapper;

    public PassportService(IPassportRepository passportRepository, PassportMapper mapper)
    {
        _passportRepository = passportRepository;
        _mapper = mapper;
    }

    public async Task<Guid> CreatePassportMeta(Guid userId, PassportMetaDto dto)
    {
        var newPassport = _mapper.FromDto(dto);
        newPassport.Id = userId;
        
        var id = await _passportRepository.CreateAsync(newPassport);

        return id;
    }

    public async Task<PassportMetaDto> GetPassportMeta(Guid userId)
    {
        var passport = await _passportRepository.GetByIdAsync(userId);
        return _mapper.ToDto(passport);
    }

    public async Task UpdatePassportMeta(Guid userId, PassportMetaDto dto)
    {
        await _passportRepository.UpdateAsync(_mapper.FromDto(dto));
    }

    public async Task DeletePassportMeta(Guid userId)
    {
        await _passportRepository.DeleteAsync(userId);
    }
}