using Cinema.Domain.Entities;
using Cinema.Domain.ValueObjects;
using Cinema.Application.DTOS;
using Cinema.Application.Abstractions.IRepositories;
using Cinema.Application.Services.IServices;
using AutoMapper;
using Cinema.Domain.Policies;
using System.Runtime.CompilerServices;
using FluentResults;
using Cinema.Application.Exceptions;
using Cinema.Domain.Exceptions;

namespace Cinema.Application.Services;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;
    private readonly IFilmRepository _filmRepository;
    private readonly IMapper _mapper;
    private readonly SessionPolicy _sessionPolicy;
    private readonly IHallRepository _hallRepository;

    public SessionService(ISessionRepository sessionRepository, IMapper mapper, IFilmRepository filmRepository, SessionPolicy sessionPolicy, IHallRepository hallRepository)
    {
        _sessionRepository = sessionRepository;
        _mapper = mapper;
        _filmRepository = filmRepository;
        _sessionPolicy = sessionPolicy;
        _hallRepository = hallRepository;
    }

    public async Task<Result<SessionResponse>> CreateAsync(CreateSessionRequest sessionDto, CancellationToken token)
    {
        var film = await _filmRepository.GetByIdAsync(sessionDto.FilmId);

        if (film is null) return Result.Fail("Film not Found");

        Session session = new(sessionDto.HallId, sessionDto.FilmId, Duration.Create(sessionDto.Start, film.Duration));
        session.AttachFilm(film);

        var timeSlots = new List<SessionTimeSlot>();

        await foreach (var ses in _sessionRepository.GetByHallIdAsync(session.HallId, token))
        {
            timeSlots.Add(new SessionTimeSlot(ses.Duration.Start, ses.Duration.End));
        }

        try
        {
            _sessionPolicy.Check(session.Duration.Start, session.Duration.End, timeSlots.AsReadOnly());
        }
        catch (BusinessRuleException ex)
        {
            return Result.Fail(ex.Message);
        }

        try
        {
            await _sessionRepository.CreateAsync(session, token);

            return Result.Ok(_mapper.Map<SessionResponse>(session));
        }
        catch (ApplicationPersistenceException ex)
        {
            return Result.Fail(ex.Message);
        }
    }

    public async IAsyncEnumerable<SessionResponse> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var session in _sessionRepository.GetAllAsync(token))
        {
            yield return _mapper.Map<SessionResponse>(session);
        }
    }

    public async Task<Result> DeleteAsync(Guid id, CancellationToken token)
    {
        try
        {
            var result = await _sessionRepository.DeleteAsync(id);

            return result == true ? Result.Ok() : Result.Fail("Session not found");
        }
        catch (ApplicationPersistenceException ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}
