namespace Cinema.Application.DTOS;

public record CreateBookingRequest
{
    public Guid SessionId { get; init; }
    public List<Guid> Seats { get; init; } = new();

    public CreateBookingRequest() { }

    public CreateBookingRequest(Guid sessionId, List<Guid> seats)
    {
        SessionId = sessionId;
        Seats = seats;
    }
}

