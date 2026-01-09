using Cinema.Domain.ValueObjects;
using Cinema.Domain.Enums;

namespace Cinema.Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public Role Role { get; set; }

    public Wallet Wallet { get; set; } = new(0);

    public List<Booking> Bookings { get; set; } = new();
}
