using File = Enrolly.Documents.Domain.Entities.File;
using Enrolly.Documents.Application.DTOs;
using Enrolly.Documents.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Documents.Application.Mappers;

[Mapper]
public partial class PassportMapper
{
    public partial Passport FromDto(PassportMetaDto dto);
    public partial Passport FromDto(UpdatePassportRequestDto dto);
    public partial PassportMetaDto ToDto(Passport dto);
    
    public partial IEnumerable<Passport> FromDtos(IEnumerable<PassportMetaDto> dto);
    public partial IEnumerable<PassportMetaDto> ToDtos(IEnumerable<Passport> dto);
    
    public partial IEnumerable<FileDto> MapFilesToDtos(IEnumerable<File> dtos);
}