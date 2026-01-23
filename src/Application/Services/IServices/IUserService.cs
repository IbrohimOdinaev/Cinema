using Cinema.Application.DTOS;
using FluentResults;

namespace Cinema.Application.Services.IServices;

public interface IUserService
{
    Task<Result<UserResponse>> CreateAsync(CreateUserRequest userDto, CancellationToken token = default);

    Task<Result> DeleteAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<UserResponse> GetAllAsync(CancellationToken token);

    Task<Result<UserResponse>> AddMoneyAsync(decimal amount, Guid id, CancellationToken token = default);

    Task<Result<string>> LoginAsync(LoginRequest loginDto, CancellationToken token = default);

    Task<Result> CreateAdminAsync(CancellationToken token = default);
}
