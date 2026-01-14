namespace Cinema.Infrastructure.DbEntities;

public class DbBooking
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DbUser? User { get; set; }

    public Guid SessionId { get; set; }

    public DbSession? Session { get; set; }

    public decimal Cost { get; set; }

    public List<Guid> Seats { get; set; } = new();
}
