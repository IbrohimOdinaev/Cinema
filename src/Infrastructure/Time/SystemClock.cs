using Cinema.Domain.Abstractions;

namespace Cinema.Infrastructure.Time;

public class SystemClock : IClock
{
    public DateTime Now => DateTime.UtcNow;

}
