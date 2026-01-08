using Cinema.Domain.Entities;

namespace Cinema.Application.Abstractions.IRepositories;

public interface ISeatRepository : IBaseRepository<Seat>
{
    IAsyncEnumerable<Seat> GetAllNotOccupiedAsync(CancellationToken token);
}
