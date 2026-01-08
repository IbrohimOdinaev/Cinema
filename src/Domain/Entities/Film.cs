namespace Cinema.Domain.Entities;

public class Film
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public int Duration { get; set; }
}
