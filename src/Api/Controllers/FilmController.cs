using Microsoft.AspNetCore.Mvc;
using Cinema.Application.DTOS;
using Cinema.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;

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

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateFilmRequest filmDto, CancellationToken token)
    {
        var result = await _filmService.CreateAsync(filmDto, token);

        if (result.IsSuccess)
            return Ok(result.Value);

        return BadRequest(result.Errors.First().Message);
    }
    [Authorize(Roles = "Admin")]
    [HttpGet]
    public IAsyncEnumerable<FilmResponse> GetAll(CancellationToken token)
    {
        return _filmService.GetAllAsync(token);
    }
}

