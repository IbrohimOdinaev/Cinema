using Cinema.Application.DTOS;
using FluentResults;

namespace Cinema.Application.Services.IServices;

public interface IFilmService
{
    Task<Result<FilmResponse>> CreateAsync(CreateFilmRequest filmDto, CancellationToken token = default);

    Task<Result> DeleteAsync(Guid id, CancellationToken token = default);

    IAsyncEnumerable<FilmResponse> GetAllAsync(CancellationToken token);
}
