namespace Cinema.Domain.Entities;

public class Seat
{
    public Guid Id { get; set; }

    public Guid HallId { get; set; }

    public bool IsOccupied { get; set; }

    public int Raw { get; set; }

    public int Num { get; set; }
}
