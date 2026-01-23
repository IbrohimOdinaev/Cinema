using Cinema.Domain.Exceptions;

namespace Cinema.Domain.ValueObjects;

public sealed class Duration
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private Duration(DateTime start, int filmDuration)
    {
        Start = start;
        End = start.AddMinutes(filmDuration);
    }

    public static Duration Create(DateTime start, int filmDuration)
    {
        if (filmDuration <= 0)
            throw new DomainArgumentException("Film duration can't be negative.");

        return new(start, filmDuration);
    }
}
