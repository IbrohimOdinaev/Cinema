namespace Cinema.Application.DTOS;

public record CreateSessionRequest
{
    public Guid HallId { get; init; }
    public Guid FilmId { get; init; }
    public DateTime Start { get; init; }

    public CreateSessionRequest(Guid hallId, Guid filmId, DateTime start)
    {
        HallId = hallId;
        FilmId = filmId;
        Start = start;
    }

    public CreateSessionRequest() { }
}



