namespace Cinema.Application.DTOS;

public record CreateBookingRequest(Guid UserId, Guid SessionId, List<Guid> Seats);
