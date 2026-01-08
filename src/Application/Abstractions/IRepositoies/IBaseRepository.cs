namespace Cinema.Application.Abstractions.IRepositories;

public interface IBaseRepository<T>
{
    Task<T?> GetByIdAsync(Guid id, CancellationToken token = default);

    Task<T?> UpdateAsync(T entity, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<T> GetAllAsync(CancellationToken token = default);

    Task<T?> CreateAsync(T entity, CancellationToken token = default);
}
