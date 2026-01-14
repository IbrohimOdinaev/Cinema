namespace Cinema.Application.DTOS;

public record CreateFilmRequest
{
    public string Title { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int Duration { get; init; }

    public CreateFilmRequest(string title, decimal price, int duration)
    {
        Title = title;
        Price = price;
        Duration = duration;
    }

    public CreateFilmRequest() { }
}

