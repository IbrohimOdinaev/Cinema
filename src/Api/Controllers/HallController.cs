using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.IServices;
using Cinema.Application.DTOS;

namespace Cinema.Api.Controllers;

[ApiController]
[Route("/hall")]
public class HallController : ControllerBase
{
    private readonly IHallService _hallService;
    private readonly ISeatService _seatService;

    public HallController(IHallService hallService, ISeatService seatService)
    {
        _hallService = hallService;
        _seatService = seatService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHallRequest hallDto, CancellationToken token)
    {
        var result = await _hallService.CreateAsync(hallDto, token);

        return Ok(result);
    }

    [HttpGet]
    public IAsyncEnumerable<HallResponse> GetAll(CancellationToken token)
    {
        return _hallService.GetAllAsync(token);
    }

    [HttpGet("{id:guid}/seats")]
    public IAsyncEnumerable<SeatResponse> GetSeats([FromRoute] Guid id, CancellationToken token)
    {
        return _seatService.GetHallSeatsAsync(id, token);
    }
}
