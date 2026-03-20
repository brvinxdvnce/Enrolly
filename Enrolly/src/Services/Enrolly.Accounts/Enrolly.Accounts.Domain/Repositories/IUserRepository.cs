using Enrolly.Accounts.Domain.Entities;

namespace Enrolly.Accounts.Domain.Repositories;

public interface IUserRepository
{
    Guid Add(User user);
    User GetById(Guid userId);
    void Update(User user);
    void Delete(User user);
    
    void Promote(Guid id);
    void Demote(Guid id);
}