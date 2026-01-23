using Cinema.Domain.ValueObjects;
using Cinema.Domain.Exceptions;

namespace Cinema.Domain.Entities;

public class Session
{
    public Guid Id { get; private set; }

    public Guid HallId { get; private set; }

    public Hall? Hall { get; private set; }

    public Guid FilmId { get; private set; }

    public Film? Film { get; private set; }

    public Duration Duration { get; private set; }

    public Session
    (
        Guid hallId,
        Guid filmId,
        Duration duration
    )
    {
        Id = Guid.NewGuid();
        HallId = hallId;
        FilmId = filmId;
        Duration = duration ?? throw new DomainArgumentException("Duration cannot be null");
    }

    public Session
    (
        Guid id,
        Guid hallId,
        Hall? hall,
        Guid filmId,
        Film? film,
        Duration duration
    )
    {
        Id = id;
        HallId = hallId;
        Hall = hall;
        FilmId = filmId;
        Film = film;
        Duration = duration;
    }

    public void AttachHall(Hall? hall)
    {
        if (hall is null) throw new DomainArgumentException("Cannot attach null Hall");

        Hall = hall;
    }

    public void AttachFilm(Film? film)
    {
        if (film is null) throw new DomainArgumentException("Cannot attach null Film");

        Film = film;
    }
}
