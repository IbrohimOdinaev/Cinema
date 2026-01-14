namespace Cinema.Application.DTOS;

public record UserResponse(Guid Id, string Name, string Email, decimal WalletBalance);
