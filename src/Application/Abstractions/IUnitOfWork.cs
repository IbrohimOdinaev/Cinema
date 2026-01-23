namespace Cinema.Application.Abstractions;

public interface IUnitOfWork
{
    Task BeginAsync(CancellationToken token = default);

    Task CommitAsync(CancellationToken token = default);

    Task RollbackAsync(CancellationToken token = default);

    Task SaveChangesAsync(CancellationToken token = default);
}
