namespace Cinema.Infrastructure.DbEntities;

public class DbHall
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public List<DbSeat> Seats { get; set; } = new();
}
