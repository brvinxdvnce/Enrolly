using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;

namespace Enrolly.Documents.Domain.Repositories;

public interface IPassportRepositoryV2
{
    public Task<Result<Passport>> GetByIdAsync(Guid applicantId);
    public Task<Result<Guid>> CreateAsync(Passport passport);
    public Task<Result> UpdateAsync(Passport passport);
    public Task<Result> DeleteAsync(Guid applicantId);
}