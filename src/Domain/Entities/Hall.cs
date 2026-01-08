namespace Cinema.Domain.Entities;

public class Hall
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public List<Seat> Seats { get; set; } = new();
}
