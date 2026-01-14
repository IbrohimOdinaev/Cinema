using Microsoft.AspNetCore.Mvc;
using Cinema.Application.DTOS;
using Cinema.Application.Services.IServices;

namespace Cinema.Api.Controllers;

[ApiController]
[Route("/film")]
public class FilmController : ControllerBase
{
    private readonly IFilmService _filmService;

    public FilmController(IFilmService filmService)
    {
        _filmService = filmService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFilmRequest filmDto, CancellationToken token)
    {
        var result = await _filmService.CreateAsync(filmDto, token);

        return Ok(result);
    }

    [HttpGet]
    public IAsyncEnumerable<FilmResponse> GetAll(CancellationToken token)
    {
        return _filmService.GetAllAsync(token);
    }
}

