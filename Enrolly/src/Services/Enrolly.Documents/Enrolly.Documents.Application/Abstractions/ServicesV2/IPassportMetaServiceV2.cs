using CSharpFunctionalExtensions;
using Enrolly.Documents.Application.DTOs;

namespace Enrolly.Documents.Application.Abstractions.ServicesV2;

public interface IPassportMetaServiceV2
{
    public Task<Result<Guid>> CreatePassportMeta(Guid applicantId, PassportMetaDto dto);
    public Task<Result<PassportMetaDto>> GetPassportMeta(Guid applicantId);
    public Task<Result> UpdatePassportMeta(Guid applicantId, PassportMetaDto dto);
    public Task<Result> DeletePassportMeta(Guid applicantId);
}