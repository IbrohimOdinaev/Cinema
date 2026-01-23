using Cinema.Application.DTOS;
using FluentResults;

namespace Cinema.Application.Services.IServices;

public interface IBookingService
{
    Task<Result<BookingResponse>> CreateAsync(CreateBookingRequest bookingDto, Guid userId, CancellationToken token = default);

    IAsyncEnumerable<BookingResponse> GetUserBookingsAsync(Guid id, CancellationToken token);
}
