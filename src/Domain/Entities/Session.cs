using Cinema.Domain.ValueObjects;

namespace Cinema.Domain.Entities;

public class Session
{
    public Guid Id { get; private set; }

    public Guid HallId { get; private set; }

    public Hall? Hall { get; private set; }

    public Guid FilmId { get; private set; }

    public Film? Film { get; private set; }

    public Duration Duration { get; private set; }

    public List<Booking> Bookings { get; private set; } = new();

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
        Duration = duration;
    }

    public Session
    (
        Guid id,
        Guid hallId,
        Hall? hall,
        Guid filmId,
        Film? film,
        Duration duration,
        List<Booking> bookings
    )
    {
        Id = id;
        HallId = hallId;
        Hall = hall;
        FilmId = filmId;
        Film = film;
        Duration = duration;
        Bookings = bookings;
    }

    public void AttachHall(Hall? hall)
    {
        if (Hall is not null || hall is null) throw new ArgumentException();

        Hall = hall;
    }

    public void AttachFilm(Film? film)
    {
        if (Film is not null || film is null) throw new ArgumentException();

        Film = film;
    }
}
