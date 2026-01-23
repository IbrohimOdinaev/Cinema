using Cinema.Domain.ValueObjects;
using Cinema.Domain.Exceptions;

namespace Cinema.Domain.Entities;

public class Seat
{
    public Guid Id { get; private set; }

    public Guid HallId { get; private set; }

    public bool IsOccupied { get; private set; }

    public Position Position { get; private set; }

    internal Seat(Guid hallId, Position position)
    {
        Id = Guid.NewGuid();
        HallId = hallId;
        IsOccupied = false;
        Position = position ?? throw new DomainArgumentException("Position cannot be null");
    }

    public Seat(Guid id, Guid hallId, bool isOccupied, Position position)
    {
        Id = id;
        HallId = hallId;
        IsOccupied = isOccupied;
        Position = position;
    }

    public void ChangeStatus(bool newStatus)
    {
        IsOccupied = newStatus;
    }
}
