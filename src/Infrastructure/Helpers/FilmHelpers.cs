using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.Helpers;

public static class FilmHelpers
{
    public static DbFilm ToDb(this Film film)
    {
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
        {
            Id = dbFilm.Id,
            Title = dbFilm.Title,
            Price = dbFilm.Price,
            Duration = dbFilm.Duration
        };
    }
}

