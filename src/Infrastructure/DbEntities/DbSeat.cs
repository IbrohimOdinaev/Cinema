namespace Cinema.Infrastructure.DbEntities;

public class DbSeat
{
    public Guid Id { get; set; }

    public int Raw { get; set; }

    public bool IsOccupied { get; set; }

    public int Num { get; set; }

    public Guid HallId { get; set; }
}
