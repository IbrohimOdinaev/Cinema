using Cinema.Domain.Entities;
using Cinema.Application.DTOS;

namespace Cinema.Application.Services.IServices;

public interface IUserService
{
    Task<UserResponse?> CreateAsync(CreateUserRequest userDto, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<UserResponse> GetAllAsync(CancellationToken token);

    Task<UserResponse?> AddMoneyAsync(Guid id, CancellationToken token = default);
}
