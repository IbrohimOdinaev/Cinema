namespace Cinema.Application.DTOS;

public record SessionResponse
{
    public Guid Id { get; init; }
    public Guid HallId { get; init; }
    public DateTime Start { get; init; }
    public DateTime End { get; init; }
    public string FilmTitle { get; init; } = string.Empty;

    public SessionResponse(Guid id, Guid hallId, DateTime start, DateTime end, string filmTitle)
    {
        Id = id;
        HallId = hallId;
        Start = start;
        End = end;
        FilmTitle = filmTitle;
    }

    public SessionResponse() { }
}

