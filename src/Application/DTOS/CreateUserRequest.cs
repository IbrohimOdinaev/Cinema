using Cinema.Domain.Enums;

namespace Cinema.Application.DTOS;

public record CreateUserRequest
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Role Role { get; init; }

    public CreateUserRequest(string name, string email, string password, Role role)
    {
        Name = name;
        Email = email;
        Password = password;
        Role = role;
    }

    public CreateUserRequest() { }
}

