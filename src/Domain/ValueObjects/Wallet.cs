using Cinema.Domain.Exceptions;

namespace Cinema.Domain.ValueObjects;

public class Wallet
{
    public decimal Balance { get; private set; }

    private Wallet(decimal balance)
    {
        Balance = balance;
    }

    public static Wallet Create(decimal balance)
    {
        if (balance < 0)
            throw new BusinessRuleException("Balance can't be negative.");

        return new Wallet(balance);
    }

    public void Add(decimal amount)
    {
        if (amount < 0) throw new BusinessRuleException("Can't add negative amount.");

        Balance += amount;
    }

    public void Deduct(decimal amount)
    {
        if (amount < 0) throw new BusinessRuleException("Can't deduct negative amount.");

        Balance -= amount;
    }
}
