namespace Cinema.Application.DTOS;

public record CreateBookingRequest
{
    public Guid UserId { get; init; }
    public Guid SessionId { get; init; }
    public List<Guid> Seats { get; init; } = new();

    public CreateBookingRequest() { }

    public CreateBookingRequest(Guid userId, Guid sessionId, List<Guid> seats)
    {
        UserId = userId;
        SessionId = sessionId;
        Seats = seats;
    }
}

