namespace Cinema.Application.DTOS;

public record UserResponse
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public decimal WalletBalance { get; init; }

    public UserResponse(Guid id, string name, string email, decimal walletBalance)
    {
        Id = id;
        Name = name;
        Email = email;
        WalletBalance = walletBalance;
    }

    public UserResponse() { }
}

