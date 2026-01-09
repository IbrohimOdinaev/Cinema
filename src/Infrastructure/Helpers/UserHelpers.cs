using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;
using Cinema.Domain.ValueObjects;

namespace Cinema.Infrastructure.Helpers;

public static class UserHelpers
{
    public static DbUser ToDb(this User user)
    {
        return new DbUser
        {
            Id = user.Id,
            Name = user.Name,
            PasswordHash = user.PasswordHash,
            Role = user.Role,
            WalletBalance = user.Wallet.Balance,
            Bookings = user.Bookings.Select(b => b.ToDb()).ToList()
        };
    }

    public static User ToDomain(this DbUser dbUser)
    {
        return new User
        {
            Id = dbUser.Id,
            Name = dbUser.Name,
            PasswordHash = dbUser.PasswordHash,
            Role = dbUser.Role,
            Wallet = new Wallet(dbUser.WalletBalance),
            Bookings = dbUser.Bookings.Select(b => b.ToDomain()).ToList()
        };
    }
}

