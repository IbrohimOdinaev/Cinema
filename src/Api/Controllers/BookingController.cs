using Microsoft.AspNetCore.Mvc;
using Cinema.Application.DTOS;
using Cinema.Application.Services.IServices;

namespace Cinema.Api.Controllers;

[ApiController]
[Route("/booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest bookingDto, CancellationToken token)
    {
        var result = await _bookingService.CreateAsync(bookingDto, token);

        return Ok(result);
    }

    [HttpGet("user/{id:guid}")]
    public IAsyncEnumerable<BookingResponse> GetUserBookings([FromRoute] Guid id, CancellationToken token)
    {
        return _bookingService.GetUserBookingsAsync(id, token);
    }
}
