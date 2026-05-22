using CSharpFunctionalExtensions;
    
namespace Enrolly.Shared.Logging.Utils.Database;

public interface IResultedGenericRepository<TEntity, TKey>
{
    public Task<Result<TKey>> AddAsync(TEntity entity, CancellationToken ct = default);
    public Task<Result<TEntity>> UpdateAsync(TEntity entity, CancellationToken ct = default);
    public Task<Result<TEntity>> GetByIdAsync(TKey id, CancellationToken ct = default);
    public Task<Result<IReadOnlyCollection<TEntity>>> GetAllAsync(CancellationToken ct = default);
    public Task<CSharpFunctionalExtensions.Result> DeleteByIdAsync(TKey id, CancellationToken ct = default);
}