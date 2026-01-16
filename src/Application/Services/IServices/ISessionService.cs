using Cinema.Application.DTOS;
using FluentResults;

namespace Cinema.Application.Services.IServices;

public interface ISessionService
{
    Task<Result<SessionResponse>> CreateAsync(CreateSessionRequest sessionDto, CancellationToken token = default);

    IAsyncEnumerable<SessionResponse> GetAllAsync(CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);
}
