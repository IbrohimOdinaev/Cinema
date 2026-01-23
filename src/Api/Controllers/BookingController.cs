using Microsoft.AspNetCore.Mvc;
using Cinema.Application.DTOS;
using Cinema.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Cinema.Api.Extensions;

namespace Cinema.Api.Controllers;

[Authorize]
[ApiController]
[Route("/booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [Authorize(Roles = "User")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBookingRequest bookingDto, CancellationToken token)
    {
        Guid userId = User.GetUserId();
        var result = await _bookingService.CreateAsync(bookingDto, userId, token);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors.First().Message);
    }

    [Authorize(Roles = "User")]
    [HttpGet("user/{id:guid}")]
    public IAsyncEnumerable<BookingResponse> GetUserBookings(CancellationToken token)
    {
        Guid userId = User.GetUserId();

        return _bookingService.GetUserBookingsAsync(userId, token);
    }
}
