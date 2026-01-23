using Cinema.Domain.Entities;
using Cinema.Domain.ValueObjects;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.Helpers;

public static class SessionHelpers
{
    public static DbSession ToDb(this Session session, AppDbContext context)
    {
        return new DbSession
        {
            Id = session.Id,
            HallId = session.HallId,
            FilmId = session.FilmId,
            Hall = session.Hall?.ToDb(context),
            Film = session.Film?.ToDb(context),
            Start = session.Duration.Start,
            End = session.Duration.End
        };
    }

    public static Session ToDomain(this DbSession dbSession)
    {
        return new Session
        (
            dbSession.Id,
            dbSession.HallId,
            dbSession.Hall?.ToDomain(),
            dbSession.FilmId,
            dbSession.Film?.ToDomain(),
            Duration.Create(dbSession.Start, (int)dbSession.End.Subtract(dbSession.Start).TotalMinutes)
        );
    }
}

