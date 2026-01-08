namespace Cinema.Infrastructure.DbEntities;

public class DbSession
{
    public Guid Id { get; set; }

    public Guid HallId { get; set; }

    public Guid FilmId { get; set; }

    public DbHall Hall { get; set; } = null!;

    public DbFilm Film { get; set; } = null!;

    public TimeOnly Start { get; set; }

    public TimeOnly End { get; set; }

    public List<DbBooking> Bookings { get; set; } = new();
}
