using Cinema.Application.DTOS;

namespace Cinema.Application.Services.IServices;

public interface ISessionService
{
    Task<SessionResponse?> CreateAsync(CreateSessionRequest sessionDto, CancellationToken token = default);

    IAsyncEnumerable<SessionResponse> GetAllAsync(CancellationToken token = default);
}
