using Cinema.Application.DTOS;
using Cinema.Application.Services;
using Cinema.Domain.Entities;

namespace Cinema.Application.Services.IServices;

public interface IBookingService
{
    Task<BookingResponse?> CreateAsync(CreateBookingRequest bookingDto, CancellationToken token = default);
}
