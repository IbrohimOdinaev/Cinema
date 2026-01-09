using Cinema.Domain.Enums;

namespace Cinema.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public User? User { get; set; }

    public Guid SessionId { get; set; }

    public Session? Session { get; set; }

    public decimal Cost { get; set; }

    public Status Status { get; set; }

    public List<Guid> Seats { get; set; } = new();
}
