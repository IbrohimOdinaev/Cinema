using Cinema.Domain.Entities;

namespace Cinema.Application.Abstractions.IRepositories;

public interface ISessionRepository : IBaseRepository<Session>
{
    Task<Session?> GetByIdWithNavigationPropertyAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<Session> GetByHallIdAsync(Guid id, CancellationToken token = default);
}
