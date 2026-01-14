namespace Cinema.Application.DTOS;

public record FilmResponse(Guid Id, string Title, decimal Price, int Duration);
