using Cinema.Domain.Enums;
namespace Cinema.Application.Abstractions;

public interface IJwtTokenGenerator
{
    string GenerateToken(Guid userId, string name, string email, Role role);
}
