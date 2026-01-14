using Cinema.Domain.Enums;

namespace Cinema.Application.DTOS;

public record CreateUserRequest(string Name, string Email, string Password, Role Role);
