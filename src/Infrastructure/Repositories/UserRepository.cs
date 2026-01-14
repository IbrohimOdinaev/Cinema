using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Application.Abstractions.IRepositories;
using static Cinema.Infrastructure.Helpers.UserHelpers;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken token)
        => (await _context.Users.FindAsync(id, token))?.ToDomain();

    public async Task<User?> GetByNameAsync(string name, CancellationToken token)
        => (await _context.Users.FirstOrDefaultAsync(u => EF.Functions.ILike(u.Name, $"%{name}%")))?.ToDomain();

    public async Task<User?> GetByIdWithNavigationPropertyAsync(Guid id, CancellationToken token)
        => (await _context.Users
                    .Include(u => u.Bookings)
                        .ThenInclude(b => b.Seats)
                    .Include(u => u.Bookings)
                        .ThenInclude(b => b.Session)
                    .FirstOrDefaultAsync(u => u.Id == id))
                    ?.ToDomain();

    public async IAsyncEnumerable<User> GetAllAsync([EnumeratorCancellation] CancellationToken token)
    {
        await foreach (var user in _context.Users)
        {
            yield return user.ToDomain();
        }
    }

    public async Task<User?> CreateAsync(User entity, CancellationToken token)
    {
        DbUser dbUser = entity.ToDb();

        await _context.Users.AddAsync(dbUser, token);
        await _context.SaveChangesAsync();

        return entity;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken token)
    {
        var user = await _context.Users.FindAsync(id, token);

        if (user is null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(token);

        return true;
    }

    public async Task<User?> UpdateAsync(User user, CancellationToken token)
    {
        var dbUser = await _context.Users.FindAsync(user.Id, token);

        if (dbUser is null) return null;

        var toAdd = BookingsToAdd(user.ToDb(), dbUser);
        var toRemove = BookingsToRemove(user.ToDb(), dbUser);

        dbUser.Bookings.RemoveAll(s => toRemove.Contains(s));
        dbUser.Bookings.AddRange(toAdd);

        dbUser.Name = user.Name;
        dbUser.PasswordHash = user.PasswordHash;
        dbUser.WalletBalance = user.Wallet.Balance;

        await _context.SaveChangesAsync(token);

        return user;
    }
}
