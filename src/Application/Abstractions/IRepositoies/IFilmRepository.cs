using Cinema.Domain.Entities;

namespace Cinema.Application.Abstractions.IRepositories;

public interface IFilmRepository : IBaseRepository<Film>
{
    Task<Film?> GetByTitleAsync(string title, CancellationToken token = default);
}
