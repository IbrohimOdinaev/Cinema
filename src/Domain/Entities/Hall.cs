using Cinema.Domain.ValueObjects;

namespace Cinema.Domain.Entities;

public class Hall
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public IReadOnlyList<Seat> Seats { get; }

    public Hall(string title, int raws, int columns)
    {
        var seats = new List<Seat>();

        for (int r = 1; r <= raws; r++)
        {
            for (int c = 1; c <= columns; c++)
            {
                seats.Add(new Seat(Id, Position.Create(r, c)));
            }
        }

        Id = Guid.NewGuid();
        Title = title;
        Seats = seats;
    }

    public Hall(Guid id, string title, List<Seat> seats)
    {
        Id = id;
        Title = title;
        Seats = seats;
    }
}
