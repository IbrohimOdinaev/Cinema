using Cinema.Domain.Enums;

namespace Cinema.Infrastructure.DbEntities;

public class DbUser
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public Role Role { get; set; }

    public decimal WalletBalance { get; set; }

    public List<DbBooking> Bookings { get; set; } = new();
}
