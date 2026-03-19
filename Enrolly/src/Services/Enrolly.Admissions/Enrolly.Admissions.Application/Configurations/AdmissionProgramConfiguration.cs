using Enrolly.Admissions.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Enrolly.Admissions.Application.Configurations;

public class AdmissionProgramConfiguration : IEntityTypeConfiguration<AdmissionProgram>
{
    public void Configure(EntityTypeBuilder<AdmissionProgram> builder)
    {
        throw new NotImplementedException();
    }
}