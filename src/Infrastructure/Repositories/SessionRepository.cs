using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Application.Abstractions.IRepositories;
using static Cinema.Infrastructure.Helpers.SessionHelpers;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.Helpers;
using Cinema.Application.Abstractions;

namespace Cinema.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _uow;

    public SessionRepository(AppDbContext context, IUnitOfWork uow)
    {
        _context = context;
        _uow = uow;
    }

    public async Task<Session?> GetByIdAsync(Guid id, CancellationToken token)
        => (await _context.Sessions.FindAsync(id, token))?.ToDomain();

    public async Task<Session?> GetByIdWithNavigationPropertyAsync(Guid id, CancellationToken token)
    {
        return (await _context.Sessions
                                .Include(s => s.Hall)
                                  .ThenInclude(h => h!.Seats)
                                .Include(s => s.Film)
                                .FirstOrDefaultAsync(s => s.Id == id))?
                                .ToDomain();
    }

    public async IAsyncEnumerable<Session> GetByHallIdAsync(Guid id, [EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var session in _context.Sessions.Include(s => s.Film).Where(s => s.HallId == id).AsAsyncEnumerable())
        {
            yield return session.ToDomain();
        }
    }

    public async IAsyncEnumerable<Session> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var session in _context.Sessions.Include(s => s.Film).AsAsyncEnumerable())
        {
            yield return session.ToDomain();
        }
    }

    public async Task<Session?> CreateAsync(Session entity, CancellationToken token)
    {
        DbSession dbSession = entity.ToDb(_context);

        await _context.Sessions.AddAsync(dbSession, token);

        await _uow.SaveChangesAsync(token);
        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var session = await _context.Sessions.FindAsync(id, token);

        if (session is null) return false;

        _context.Sessions.Remove(session);

        await _uow.SaveChangesAsync(token);

        return true;
    }

    public async Task<Session?> UpdateAsync(Session session, CancellationToken token)
    {
        var dbSession = await _context.Sessions.FindAsync(session.Id, token);

        if (dbSession is null) return null;

        dbSession.HallId = session.HallId;
        dbSession.FilmId = session.FilmId;

        await _uow.SaveChangesAsync(token);
        return session;
    }
}
