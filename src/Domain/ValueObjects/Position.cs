using Cinema.Domain.Exceptions;

namespace Cinema.Domain.ValueObjects;

public class Position
{
    public int Raw { get; }
    public int Num { get; }

    private Position(int raw, int num)
    {
        Raw = raw;
        Num = num;
    }

    public static Position Create(int raw, int num)
    {
        if (raw <= 0 || num <= 0)
            throw new DomainArgumentException("Position can't be negative.");

        return new Position(raw, num);
    }
}
