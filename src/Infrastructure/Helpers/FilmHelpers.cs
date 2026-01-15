using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.Helpers;

public static class FilmHelpers
{
    public static DbFilm ToDb(this Film film, AppDbContext context)
    {
        var dbFilm = context.Films.Local.FirstOrDefault(f => f.Id == film.Id);

        if (dbFilm is not null)
        {
            return dbFilm;
        }
        return new DbFilm
        {
            Id = film.Id,
            Title = film.Title,
            Price = film.Price,
            Duration = film.Duration
        };
    }

    public static Film ToDomain(this DbFilm dbFilm)
    {
        return new Film
        (
            dbFilm.Id,
            dbFilm.Title,
            dbFilm.Price,
            dbFilm.Duration
        );
    }
}

