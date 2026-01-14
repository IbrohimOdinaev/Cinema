using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Application.Abstractions.IRepositories;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using Cinema.Infrastructure.Helpers;


namespace Cinema.Infrastructure.Repositories;


public class BookingRepository : IBookingRepository
{
    private readonly AppDbContext _context;

    public BookingRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken token)
      => (await _context.Bookings.FindAsync(id, token))?.ToDomain();

    public async Task<Booking?> GetByIdWithNavigationPropertiesAsync(Guid id, CancellationToken token)
      => (await _context.Bookings
                                              .Include(b => b.Session)
                                                .ThenInclude(s => s!.Film)
                                              .Include(b => b.Session)
                                                .ThenInclude(s => s!.Hall)
                                              .Include(b => b.User)
                                              .FirstOrDefaultAsync(b => b.Id == id, token))
                                              ?.ToDomain();

    public async IAsyncEnumerable<Booking> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var booking in _context.Bookings
                                                .AsNoTracking()
                                                .AsAsyncEnumerable())
        {
            yield return booking.ToDomain();
        }
    }

    public async IAsyncEnumerable<Booking> GetByUserIdAsync(Guid id, [EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var booking in _context.Bookings
                                                .AsNoTracking()
                                                .Where(b => b.UserId == id)
                                                .AsAsyncEnumerable())
        {
            yield return booking.ToDomain();
        }
    }

    public async Task<Booking?> CreateAsync(Booking entity, CancellationToken token)
    {
        DbBooking dbBooking = entity.ToDb(_context);

        await _context.Bookings.AddAsync(dbBooking, token);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<Booking?> UpdateAsync(Booking entity, CancellationToken token)
    {
        var booking = await _context.Bookings.FindAsync(entity.Id, token);

        if (booking is null) return null;

        booking.Seats = entity.Seats;

        await _context.SaveChangesAsync(token);

        return booking.ToDomain();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var booking = await _context.Bookings.FindAsync(id, token);

        if (booking is null) return false;

        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();

        return true;
    }


}



