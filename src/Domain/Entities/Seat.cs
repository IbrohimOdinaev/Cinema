namespace Cinema.Domain.Entities;

public class Seat
{
    public Guid Id { get; set; }

    public Guid HallId { get; set; }

    public bool IsOcuupied { get; set; }

    public int Row { get; set; }

    public int Num { get; set; }
}
