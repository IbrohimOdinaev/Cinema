using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Application.Abstractions.IRepositories;
using static Cinema.Infrastructure.Helpers.HallHelpers;
using System.Runtime.CompilerServices;

namespace Cinema.Infrastructure.Repositories;

public class HallRepository : IHallRepository
{
    private readonly AppDbContext _context;

    public HallRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Hall?> GetByIdAsync(Guid id, CancellationToken token)
        => (await _context.Halls.FindAsync(id, token))?.ToDomain();

    public async IAsyncEnumerable<Hall> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var hall in _context.Halls)
        {
            yield return hall.ToDomain();
        }
    }

    public async Task<Hall?> CreateAsync(Hall entity, CancellationToken token)
    {
        DbHall dbHall = entity.ToDb(_context);

        await _context.Halls.AddAsync(dbHall, token);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var hall = await _context.Halls.FindAsync(id, token);

        if (hall is null) return false;

        _context.Halls.Remove(hall);
        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<Hall?> UpdateAsync(Hall hall, CancellationToken token)
    {
        var dbHall = await _context.Halls.FindAsync(hall.Id, token);

        if (dbHall is null) return null;

        dbHall.Title = hall.Title;
        await _context.SaveChangesAsync(token);

        return hall;
    }
}
