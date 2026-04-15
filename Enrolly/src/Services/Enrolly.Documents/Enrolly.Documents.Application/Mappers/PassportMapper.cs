using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Documents.Application.Mappers;

[Mapper]
public partial class PassportMapper
{
    public partial Passport FromDto(PassportMetaDto dto);
    public partial PassportMetaDto ToDto(Passport dto);
    
    public partial IEnumerable<Passport> FromDtos(IEnumerable<PassportMetaDto> dto);
    public partial IEnumerable<PassportMetaDto> ToDtos(IEnumerable<Passport> dto);
}