using Cinema.Application.DTOS;
using Cinema.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Cinema.Api.Controllers;

[ApiController]
[Route("/session")]
public class SessionController : ControllerBase
{
    private readonly ISessionService _sessionService;
    private readonly IFilmService _filmService;
    private readonly IHallService _hallService;

    public SessionController(ISessionService sessionService, IFilmService filmService, IHallService hallService)
    {
        _sessionService = sessionService;
        _filmService = filmService;
        _hallService = hallService;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSessionRequest sessionDto, CancellationToken token)
    {
        var result = await _sessionService.CreateAsync(sessionDto, token);

        if (result.IsFailed)
        {
            return Ok(result.Errors.First().Message);
        }

        return Ok(result.Value);

    }

    [Authorize(Roles = "User, Admin")]
    [HttpGet]
    public IAsyncEnumerable<SessionResponse> GetAll(CancellationToken token)
    {
        return _sessionService.GetAllAsync(token);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken token)
    {
        var result = await _sessionService.DeleteAsync(id, token);

        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Errors.First().Message);
    }
}


