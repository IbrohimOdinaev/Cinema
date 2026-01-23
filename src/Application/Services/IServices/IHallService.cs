using Cinema.Application.DTOS;
using FluentResults;

namespace Cinema.Application.Services.IServices;

public interface IHallService
{
    Task<Result<HallResponse>> CreateAsync(CreateHallRequest hallDto, CancellationToken token = default);

    Task<Result> DeleteAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<HallResponse> GetAllAsync(CancellationToken token);
}
