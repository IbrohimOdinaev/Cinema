namespace Cinema.Application.DTOS;

public record SessionResponse(Guid Id, Guid HallId, TimeOnly Start, TimeOnly End, string FilmTitle);
