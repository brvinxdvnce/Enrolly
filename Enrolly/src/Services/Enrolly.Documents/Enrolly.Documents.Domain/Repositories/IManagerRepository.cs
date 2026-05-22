using CSharpFunctionalExtensions;
using Enrolly.Documents.Domain.Entities;
using Enrolly.Shared.Logging.Utils.Models;

namespace Enrolly.Documents.Domain.Repositories;

public interface IManagerRepository
{
    public Task<Result<Guid>> Add(Manager manager);
    public Task<Result<Manager>> GetById(Guid managerId);
    public Task<Result> DeleteById(Guid managerId);
    public Task<Result> Update(Manager manager);
    public Task<Result> ChangeGrade(Guid managerId, ManagerGrade grade);   
}