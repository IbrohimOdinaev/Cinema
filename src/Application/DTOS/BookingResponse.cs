namespace Cinema.Application.DTOS;

public record BookingResponse(Guid Id, Guid SessionId, string FilmTitle, decimal cost, List<RawNum> Positions);
