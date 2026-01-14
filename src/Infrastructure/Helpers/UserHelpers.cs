using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Domain.ValueObjects;

namespace Cinema.Infrastructure.Helpers;

public static class UserHelpers
{
    public static List<DbBooking> BookingsToRemove(DbUser newUser, DbUser oldUser)
    {
        return oldUser.Bookings.Where(s => !newUser.Bookings.Contains(s)).ToList();
    }

    public static List<DbBooking> BookingsToAdd(DbUser newUser, DbUser oldUser)
    {
        return newUser.Bookings.Where(s => !oldUser.Bookings.Contains(s)).ToList();
    }


    public static DbUser ToDb(this User user)
    {
        return new DbUser
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email.Value,
            PasswordHash = user.PasswordHash,
            Role = user.Role,
            WalletBalance = user.Wallet.Balance,
            Bookings = user.Bookings.Select(b => b.ToDb()).ToList()
        };
    }

    public static User ToDomain(this DbUser dbUser)
    {
        return new User
        (
            dbUser.Id,
            dbUser.Name,
            Email.Create(dbUser.Email),
            dbUser.PasswordHash,
            dbUser.Role,
            Wallet.Create(dbUser.WalletBalance),
            dbUser.Bookings.Select(b => b.ToDomain()).ToList()
        );
    }
}

