using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Accounts.Application.Mappers;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.None)]
public partial class UserMapper
{
    public partial UserViewDto ToView(User user);
    //public partial User FromDto(UpdateUserDto dto);
    //public partial UpdateUserDto ToDto(User user);
       
    //public partial IEnumerable<User> FromDtos(IEnumerable<UpdateUserDto> dto);
    //public partial IEnumerable<UpdateUserDto> ToDtos(IEnumerable<User> dto);
}