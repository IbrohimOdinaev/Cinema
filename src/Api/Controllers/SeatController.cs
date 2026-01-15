using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.IServices;
using Cinema.Application.DTOS;

namespace Cinema.Api.Controlleres;

[ApiController]
[Route("/seat")]
public class SeatController : ControllerBase
{
    private readonly ISeatService _seatService;

    public SeatController(ISeatService seatService)
    {
        _seatService = seatService;
    }

    [HttpGet("{id:guid}")]
    public IAsyncEnumerable<SeatResponse> GetAll([FromRoute] Guid id, CancellationToken token)
    {
        return _seatService.GetHallSeatsAsync(id, token);
    }
}

