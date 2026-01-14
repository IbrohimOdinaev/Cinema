using Cinema.Domain.Entities;
using Cinema.Domain.ValueObjects;
using Cinema.Application.DTOS;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Services.IServices;
using AutoMapper;
using System.Runtime.CompilerServices;

namespace Cinema.Application.Services;

public class SessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly IMapper _mapper;

    public SessionService(ISessionRepository sessionRepository, IMapper mapper, IFilmRepository filmRepository)
    {
        _sessionRepository = sessionRepository;
        _mapper = mapper;
        _filmRepository = filmRepository;
    }

    public async Task<SessionResponse?> CreateAsync(CreateSessionRequest sessionDto, CancellationToken token)
    {
        var film = await _filmRepository.GetByIdAsync(sessionDto.FilmId);

        if (film is null) return null;

        Session session = new(sessionDto.HallId, sessionDto.FilmId, Duration.Create(sessionDto.Start, film.Duration));
        session.AttachFilm(film);

        try
        {
            await _sessionRepository.CreateAsync(session, token);

            return _mapper.Map<SessionResponse>(session);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async IAsyncEnumerable<SessionResponse> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var session in _sessionRepository.GetAllAsync(token))
        {
            yield return _mapper.Map<SessionResponse>(session);
        }
    }
}
