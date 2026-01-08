namespace Cinema.Domain.ValueObjects;

public class Wallet
{
    public decimal Balance { get; private set; } = 0;

    public Wallet() { }

    public Wallet(decimal balance)
    {
        Balance = balance;
    }

    public void Add(decimal amount)
    {
        if (amount < 0) throw new InvalidOperationException();

        Balance += amount;
    }

    public void Deduct(decimal amount)
    {
        if (amount < 0) throw new InvalidOperationException();

        Balance -= amount;
    }
}
