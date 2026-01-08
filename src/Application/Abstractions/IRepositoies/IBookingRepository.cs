using Cinema.Domain.Entities;

namespace Cinema.Application.Abstractions.IRepositories;

public interface IBookingRepository : IBaseRepository<Booking>
{
    IAsyncEnumerable<Booking> GetByUserIdAsync(Guid id, CancellationToken token = default);

    Task<Booking?> GetByIdWithNestedObjects(Guid id, CancellationToken token = default);
}
