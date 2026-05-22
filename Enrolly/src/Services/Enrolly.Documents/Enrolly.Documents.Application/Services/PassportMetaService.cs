using Enrolly.Documents.Application.Abstractions;
using Enrolly.Documents.Application.Abstractions.Services;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Application.Mappers;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Documents.Domain.Repositories;

namespace Enrolly.Documents.Application.Services;

public class PassportMetaService : IPassportMetaService
{
    private readonly IPassportRepository _passportRepository;
    private readonly PassportMapper _mapper;

    public PassportMetaService(IPassportRepository passportRepository, PassportMapper mapper)
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

    public async Task UpdatePassportMeta(Guid userId, UpdatePassportRequestDto dto)
    {
        var passport = _mapper.FromDto(dto);
        passport.Id = userId;
        await _passportRepository.UpdateAsync(passport);
    }

    public async Task DeletePassportMeta(Guid userId)
    {
        await _passportRepository.DeleteAsync(userId);
    }
}