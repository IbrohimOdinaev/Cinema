namespace Cinema.Infrastructure.DbEntities;

public class DbFilm
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public Decimal Price { get; set; }

    public int Duration { get; set; }
}
