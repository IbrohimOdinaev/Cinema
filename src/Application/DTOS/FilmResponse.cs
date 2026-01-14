namespace Cinema.Application.DTOS;


public record FilmResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Duration { get; init; }

    public FilmResponse(Guid id, string title, decimal price, int duration)
    {
        Id = id;
        Title = title;
        Price = price;
        Duration = duration;
    }

    public FilmResponse() { }
}
