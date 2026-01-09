namespace Cinema.Domain.Entities;

public class Session
{
    public Guid Id { get; set; }

    public Guid HallId { get; set; }

    public Guid FilmId { get; set; }

    public Film? Film { get; set; }

    public Hall? Hall { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }

    public List<Booking> Bookings { get; set; } = new();
}
