using Cinema.Application.DTOS;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Services.IServices;
using Cinema.Domain.Entities;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace Cinema.Application.Services;

public class FilmService
{
    private readonly IFilmRepository _filmRepository;
    private readonly IMapper _mapper;

    public FilmService(IFilmRepository filmRepository, IMapper mapper)
    {
        _filmRepository = filmRepository;
        _mapper = mapper;
    }

    public async IAsyncEnumerable<FilmResponse> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var film in _filmRepository.GetAllAsync(token))
        {
            yield return _mapper.Map<FilmResponse>(film);
        }
    }

    public async Task<Film?> CreateAsync(CreateFilmRequest filmDto, CancellationToken token)
    {
        var film = _mapper.Map<Film>(filmDto);

        try
        {
            await _filmRepository.CreateAsync(film, token);

            return film;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        try
        {
            bool result = await _filmRepository.DeleteAsync(id, token);

            return result;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
