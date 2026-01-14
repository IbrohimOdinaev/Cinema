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

        return Ok(result);
    }

    [HttpGet]
    public IAsyncEnumerable<SessionResponse> GetAll(CancellationToken token)
    {
        return _sessionService.GetAllAsync(token);
    }


}


