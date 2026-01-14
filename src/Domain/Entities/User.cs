using Cinema.Domain.ValueObjects;
using Cinema.Domain.Enums;

namespace Cinema.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public Email Email { get; private set; }

    public string PasswordHash { get; private set; } = string.Empty;

    public Role Role { get; }

    public Wallet Wallet { get; private set; }

    public List<Booking> Bookings { get; private set; } = new();

    public User
    (
        string name,
        Email email,
        string passwordHash,
        Role role
    )
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Wallet = Wallet.Create(0);
    }

    public User
    (
        Guid id,
        string name,
        Email email,
        string passwordHash,
        Role role,
        Wallet wallet,
        List<Booking> bookings)
    {
        Id = id;
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
        Wallet = wallet;
        Bookings = bookings;
    }

    public void ChangeName(string newName)
    {
        if (string.IsNullOrWhiteSpace(newName))
            throw new ArgumentException();

        Name = newName;
    }

    public void ChangeEmail(Email email)
    {
        Email = email;
    }

    public void ChangePasswordHash(string newPasswordHash)
    {
        if (string.IsNullOrWhiteSpace(newPasswordHash))
            throw new ArgumentException();

        PasswordHash = newPasswordHash;
    }
}
