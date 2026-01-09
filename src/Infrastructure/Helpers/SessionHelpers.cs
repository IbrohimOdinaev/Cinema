using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.Helpers;

public static class SessionHelpers
{
    public static DbSession ToDb(this Session session)
    {
        return new DbSession
        {
            Id = session.Id,
            HallId = session.HallId,
            FilmId = session.FilmId,
            Hall = session.Hall?.ToDb(),
            Film = session.Film?.ToDb(),
            Start = session.Start,
            End = session.End,
            Bookings = session.Bookings.Select(b => b.ToDb()).ToList()
        };
    }

    public static Session ToDomain(this DbSession dbSession)
    {
        return new Session
        {
            Id = dbSession.Id,
            HallId = dbSession.HallId,
            FilmId = dbSession.FilmId,
            Hall = dbSession.Hall?.ToDomain(),
            Film = dbSession.Film?.ToDomain(),
            Start = dbSession.Start,
            End = dbSession.End,
            Bookings = dbSession.Bookings.Select(b => b.ToDomain()).ToList()
        };
    }
}

