using Cinema.Domain.Entities;

namespace Cinema.Application.Abstractions.IRepositories;

public interface ISeatRepository : IBaseRepository<Seat>
{
    IAsyncEnumerable<Seat> GetByHallIdAsync(Guid id, CancellationToken token);
}
