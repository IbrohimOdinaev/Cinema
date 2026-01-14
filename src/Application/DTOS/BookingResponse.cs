namespace Cinema.Application.DTOS;

public record BookingResponse
{
    public Guid Id { get; init; }
    public Guid SessionId { get; init; }
    public string FilmTitle { get; init; } = string.Empty;
    public decimal Cost { get; init; }
    public List<RawNum> Positions { get; init; } = new();

    public BookingResponse() { }

    public BookingResponse(Guid id, Guid sessionId, string filmTitle, decimal cost, List<RawNum> positions)
    {
        Id = id;
        SessionId = sessionId;
        FilmTitle = filmTitle;
        Cost = cost;
        Positions = positions;
    }
}

