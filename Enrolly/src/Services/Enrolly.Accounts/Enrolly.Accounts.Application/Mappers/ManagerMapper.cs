using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Accounts.Application.Mappers;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.None)]
public partial class ManagerMapper
{
    public partial Manager FromDto(ManagerDto dto);
    public partial ManagerDto ToDto(Manager dto);
       
    public partial IEnumerable<Manager> FromDtos(IEnumerable<ManagerDto> dto);
    public partial IEnumerable<ManagerDto> ToDtos(IEnumerable<Manager> dto);
}
