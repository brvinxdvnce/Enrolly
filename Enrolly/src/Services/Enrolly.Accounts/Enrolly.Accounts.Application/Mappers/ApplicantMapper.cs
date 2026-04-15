using Enrolly.Accounts.Application.DTOs;
using Enrolly.Accounts.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace Enrolly.Accounts.Application.Mappers;

[Mapper(RequiredEnumMappingStrategy = RequiredMappingStrategy.None)]
public partial class ApplicantMapper
{
    public partial Applicant FromDto(ApplicantDto dto);
    public partial ApplicantDto ToDto(Applicant dto);
       
    public partial IEnumerable<Applicant> FromDtos(IEnumerable<ApplicantDto> dto);
    public partial IEnumerable<ApplicantDto> ToDtos(IEnumerable<Applicant> dto);
}