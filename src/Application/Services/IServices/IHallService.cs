using Cinema.Domain.Entities;
using Cinema.Application.DTOS;

namespace Cinema.Application.Services.IServices;

public interface IHallService
{
    Task<HallResponse?> CreateAsync(CreateHallRequest hallDto, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<HallResponse> GetAllAsync(CancellationToken token);
}
