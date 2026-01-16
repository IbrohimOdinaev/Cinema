using Cinema.Application.DTOS;
using Cinema.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;

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

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSessionRequest sessionDto, CancellationToken token)
    {
        var result = await _sessionService.CreateAsync(sessionDto, token);

        if (result.IsFailed)
        {
            return Ok(result.Errors.FirstOrDefault());
        }

        return Ok(result.Value);

    }

    [HttpGet]
    public IAsyncEnumerable<SessionResponse> GetAll(CancellationToken token)
    {
        return _sessionService.GetAllAsync(token);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id, CancellationToken token)
    {
        return Ok(await _sessionService.DeleteAsync(id, token));
    }

}


