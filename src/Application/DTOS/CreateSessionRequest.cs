namespace Cinema.Application.DTOS;

public record CreateSessionRequest(Guid HallId, Guid FilmId, DateTime Start);
