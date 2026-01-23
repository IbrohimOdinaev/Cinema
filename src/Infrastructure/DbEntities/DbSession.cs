namespace Cinema.Infrastructure.DbEntities;

public class DbSession
{
    public Guid Id { get; set; }

    public Guid HallId { get; set; }

    public Guid FilmId { get; set; }

    public DbHall? Hall { get; set; }

    public DbFilm? Film { get; set; }

    public DateTime Start { get; set; }

    public DateTime End { get; set; }
}
