using Cinema.Domain.Abstractions;

namespace Cinema.Infrastructure.Time;

public class FixedTime : IClock
{
    private readonly DateTime _now;

    public DateTime Now => _now;

    public FixedTime(DateTime time)
    {
        _now = time;
    }
}
