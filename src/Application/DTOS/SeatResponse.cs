namespace Cinema.Application.DTOS;

public record SeatResponse(Guid Id, bool IsOccupied, int Raw, int Num);
