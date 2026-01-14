namespace Cinema.Domain.ValueObjects;

public sealed class Email
{
    public string Value { get; }

    private Email(string value)
    {
        Value = value.ToLowerInvariant();
    }

    public static Email Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException();

        if (!IsValidEmail(value))
            throw new ArgumentException();

        return new Email(value);
    }

    public static bool IsValidEmail(string email)
    {
        try
        {
            var address = new System.Net.Mail.MailAddress(email);

            return address.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Email other) return false;

        return Value.ToLowerInvariant() == other.Value.ToLowerInvariant();
    }

    public override int GetHashCode() => Value.ToLowerInvariant().GetHashCode();
}
