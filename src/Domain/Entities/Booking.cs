using Cinema.Domain.Enums;

namespace Cinema.Domain.Entities;

public class Booking
{
    public Guid Id { get; private set; }

    public Guid UserId { get; private set; }

    public User? User { get; private set; }

    public Guid SessionId { get; private set; }

    public Session? Session { get; private set; }

    public decimal Cost { get; private set; } = 0;

    public List<Guid> Seats { get; private set; } = new();

    public Booking
    (
        Guid userId,
        Guid sessionId,
        decimal cost,
        List<Guid> seats
    )
    {
        Id = Guid.NewGuid();
        UserId = userId;
        SessionId = sessionId;
        Cost = cost;
        Seats = seats;
    }

    public Booking
    (
        Guid id,
        Guid userId,
        User? user,
        Guid sessionId,
        Session? session,
        decimal cost,
        List<Guid> seats
    )
    {
        Id = id;
        UserId = userId;
        User = user;
        SessionId = sessionId;
        Session = session;
        Cost = cost;
        Seats = seats;
    }

    public void AttachUser(User? user)
    {
        if (User is not null || user is null) throw new ArgumentException();

        User = user;
    }

    public void AttachSession(Session? session)
    {
        if (Session is not null || session is null) throw new ArgumentException();

        Session = session;
    }
}
