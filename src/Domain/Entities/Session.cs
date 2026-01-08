namespace Cinema.Domain.Entities;

public class Session
{
    public Guid Id { get; set; }

    public Guid HallId { get; set; }

    public Guid FilmId { get; set; }

    public Film Film { get; set; } = new();

    public Hall Hall { get; set; } = new();

    public TimeOnly Start { get; set; }

    public TimeOnly End { get; set; }

    public List<Booking> Bookings { get; set; } = new();
}
