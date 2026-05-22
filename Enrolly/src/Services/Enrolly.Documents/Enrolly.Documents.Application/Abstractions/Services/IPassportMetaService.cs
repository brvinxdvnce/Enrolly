using Enrolly.Documents.Application.DTOs;

namespace Enrolly.Documents.Application.Abstractions.Services;

public interface IPassportMetaService
{
    public Task<Guid> CreatePassportMeta(Guid userId, PassportMetaDto dto);
    public Task<PassportMetaDto> GetPassportMeta(Guid userId);
    public Task UpdatePassportMeta(Guid userId, UpdatePassportRequestDto dto);
    public Task DeletePassportMeta(Guid userId);
}