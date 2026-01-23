using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Application.Abstractions.IRepositories;
using static Cinema.Infrastructure.Helpers.FilmHelpers;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Helpers;
using Cinema.Application.Abstractions;

namespace Cinema.Infrastructure.Repositories;

public class FilmRepository : IFilmRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _uow;

    public FilmRepository(AppDbContext context, IUnitOfWork uow)
    {
        _context = context;
        _uow = uow;
    }

    public async Task<Film?> GetByIdAsync(Guid id, CancellationToken token)
      => (await _context.Films.FindAsync(id, token))?.ToDomain();

    public async Task<Film?> GetByTitleAsync(string title, CancellationToken token)
        => (await _context.Films.FirstOrDefaultAsync(f => EF.Functions.ILike(f.Title, $"%{title}%")))?.ToDomain();

    public async IAsyncEnumerable<Film> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var film in _context.Films)
        {
            yield return film.ToDomain();
        }
    }

    public async Task<Film?> CreateAsync(Film entity, CancellationToken token)
    {
        DbFilm dbFilm = entity.ToDb(_context);

        await _context.Films.AddAsync(dbFilm, token);
        await _uow.SaveChangesAsync(token);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var film = await _context.Films.FindAsync(id, token);

        if (film is null) return false;

        _context.Films.Remove(film);
        await _uow.SaveChangesAsync(token);
        return true;
    }

    public async Task<Film?> UpdateAsync(Film film, CancellationToken token)
    {
        var dbFilm = await _context.Films.FindAsync(film.Id, token);

        if (dbFilm is null) return null;

        dbFilm.Duration = film.Duration;
        dbFilm.Price = film.Price;
        dbFilm.Title = film.Title;

        await _uow.SaveChangesAsync(token);

        return film;
    }
}
