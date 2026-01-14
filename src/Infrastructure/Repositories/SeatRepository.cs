using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Application.Abstractions.IRepositories;
using static Cinema.Infrastructure.Helpers.SeatHelpers;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class SeatRepository : ISeatRepository
{
    private readonly AppDbContext _context;

    public SeatRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Seat?> GetByIdAsync(Guid id, CancellationToken token)
        => (await _context.Seats.FindAsync(id, token))?.ToDomain();

    public async IAsyncEnumerable<Seat> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var seat in _context.Seats)
        {
            yield return seat.ToDomain();
        }
    }

    public async IAsyncEnumerable<Seat> GetByHallIdAsync(Guid id, [EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var seat in _context.Seats.Where(s => s.HallId == id).OrderByDescending(s => s.Raw).ThenByDescending(s => s.Num).AsAsyncEnumerable())
        {
            yield return seat.ToDomain();
        }
    }

    public async Task<Seat?> CreateAsync(Seat entity, CancellationToken token)
    {
        DbSeat dbSeat = entity.ToDb(_context);

        await _context.Seats.AddAsync(dbSeat, token);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var seat = await _context.Seats.FindAsync(id, token);

        if (seat is null) return false;

        _context.Seats.Remove(seat);
        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<Seat?> UpdateAsync(Seat seat, CancellationToken token)
    {
        var dbSeat = await _context.Seats.FindAsync(seat.Id, token);

        if (dbSeat is null) return null;

        dbSeat.HallId = seat.HallId;
        dbSeat.Num = seat.Position.Num;
        dbSeat.Raw = seat.Position.Raw;
        dbSeat.IsOccupied = seat.IsOccupied;

        await _context.SaveChangesAsync(token);

        return seat;
    }
}
