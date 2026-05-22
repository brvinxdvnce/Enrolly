namespace Enrolly.Shared.Logging.Utils.Database;

public interface IGenericRepository<TEntity, TKey>
{
    public Task<TKey> AddAsync(TEntity entity, CancellationToken ct = default);
    public Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken ct = default);
    public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default);
    public Task<ICollection<TEntity>> GetAllAsync(CancellationToken ct = default);
    public Task DeleteByIdAsync(TKey id, CancellationToken ct = default);
}