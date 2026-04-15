using Enrolly.Documents.Application.DTOs;

namespace Enrolly.Documents.Application.Abstractions;

public interface IPassportService
{
    public Task<Guid> CreatePassportMeta(Guid userId, PassportMetaDto dto);
    public Task<PassportMetaDto> GetPassportMeta(Guid userId);
    public Task UpdatePassportMeta(Guid userId, PassportMetaDto dto);
    public Task DeletePassportMeta(Guid userId);
}