using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Application.Abstractions.IRepositories;
using static Cinema.Infrastructure.Helpers.SessionHelpers;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
    private readonly AppDbContext _context;

    public SessionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Session?> GetByIdAsync(Guid id, CancellationToken token)
        => (await _context.Sessions.FindAsync(id, token))?.ToDomain();

    public async Task<Session?> GetByIdWithNavigationPropertyAsync(Guid id, CancellationToken token)
    {
        return (await _context.Sessions
                                .Include(s => s.Hall)
                                  .ThenInclude(h => h!.Seats)
                                .Include(s => s.Film)
                                .Include(s => s.Bookings)
                                .FirstOrDefaultAsync(s => s.Id == id))?
                                .ToDomain();
    }
    public async IAsyncEnumerable<Session> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var session in _context.Sessions)
        {
            yield return session.ToDomain();
        }
    }

    public async Task<Session?> CreateAsync(Session entity, CancellationToken token)
    {
        DbSession dbSession = entity.ToDb(_context);

        await _context.Sessions.AddAsync(dbSession, token);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var session = await _context.Sessions.FindAsync(id, token);

        if (session is null) return false;

        _context.Sessions.Remove(session);
        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<Session?> UpdateAsync(Session session, CancellationToken token)
    {
        var dbSession = await _context.Sessions.FindAsync(session.Id, token);

        if (dbSession is null) return null;

        var toAdd = BookingsToAdd(session.ToDb(_context), dbSession);
        var toRemove = BookingsToRemove(session.ToDb(_context), dbSession);

        dbSession.Bookings.RemoveAll(s => toRemove.Contains(s));
        dbSession.Bookings.AddRange(toAdd);

        await _context.SaveChangesAsync(token);

        return session;
    }
}
