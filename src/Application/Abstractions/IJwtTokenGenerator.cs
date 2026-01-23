using Cinema.Domain.Enums;
namespace Cinema.Application.Abstractions;

public interface IJwtTokenGenerator
{
    Task<string> GenerateToken(Guid userId, string name, string email, Role role);
}
