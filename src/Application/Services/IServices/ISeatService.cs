using Cinema.Domain.Entities;
using Cinema.Application.DTOS;

namespace Cinema.Application.Services.IServices;

public interface ISeatService
{
    IAsyncEnumerable<SeatResponse> GetHallSeatsAsync(Guid id, CancellationToken token = default);
}
