using CSharpFunctionalExtensions;
using Enrolly.Admissions.Domain.Entities;
using Enrolly.Shared.Logging.Utils.Models;

namespace Enrolly.Admissions.Domain.Repositories;

public interface IManagerRepository
{
    public Task<Result<Guid>> Add(Manager manager);
    public Task<Result<Manager>> GetById(Guid managerId);
    public Task<Result> DeleteById(Guid managerId);
    public Task<Result<Manager>> Update(Manager manager);
    public Task<Result> ChangeGrade(Guid managerId, ManagerGrade grade);
}