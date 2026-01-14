using Cinema.Domain.Entities;
using Cinema.Application.DTOS;

namespace Cinema.Application.Services.IServices;

public interface IFilmService
{
    Task<FilmResponse?> CreateAsync(CreateFilmRequest filmDto, CancellationToken token = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<FilmResponse> GetAllAsync(CancellationToken token);
}
