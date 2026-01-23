using Microsoft.AspNetCore.Mvc;
using Cinema.Application.Services.IServices;
using Cinema.Application.DTOS;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHallRequest hallDto, CancellationToken token)
    {
        var result = await _hallService.CreateAsync(hallDto, token);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors.First().Message);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IAsyncEnumerable<HallResponse> GetAll(CancellationToken token)
    {
        return _hallService.GetAllAsync(token);
    }

    [Authorize(Roles = "Admin, User")]
    [HttpGet("{id:guid}/seats")]
    public IAsyncEnumerable<SeatResponse> GetSeats([FromRoute] Guid id, CancellationToken token)
    {
        return _seatService.GetHallSeatsAsync(id, token);
    }
}
