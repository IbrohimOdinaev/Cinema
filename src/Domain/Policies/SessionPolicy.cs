using Cinema.Domain.Abstractions;
using Cinema.Domain.Exceptions;

namespace Cinema.Domain.Policies;

public class SessionPolicy
{
    private readonly IClock _clock;
    private readonly ICinemaSettings _settings;

    public SessionPolicy(IClock clock, ICinemaSettings settings)
    {
        _clock = clock;
        _settings = settings;
        _settings.OpeningHour = 9;
        _settings.ClosingHour = 23;
        _settings.MinMinuteAddSession = 60;
        _settings.MaxDaysAddSession = 10;
    }

    public void Check(DateTime start, DateTime end, IReadOnlyCollection<SessionTimeSlot> existingSessions)
    {
        DateTime now = _clock.Now;
        //        throw new Exception($"{_settings.OpeningHour}, {_settings.ClosingHour}, {_settings.MinMinuteAddSession}, {_settings.MaxDaysAddSession}");

        DateTime lastAllowedDay = now.AddDays(_settings.MaxDaysAddSession);

        if (start < now.AddMinutes(_settings.MinMinuteAddSession))
            throw new PolicyException($"{_clock.Now} Session must add not less than {_settings.MinMinuteAddSession}");

        if (start > lastAllowedDay)
            throw new PolicyException($"Session could add before {_settings.MaxDaysAddSession} days!");

        if (!(start.Hour * 60 + start.Minute >= _settings.OpeningHour * 60 && end.Hour * 60 + end.Minute <= _settings.ClosingHour * 60 && end.Day == start.Day))
            throw new PolicyException($"Cinema is closed. Works: {_settings.OpeningHour} to {_settings.ClosingHour})");

        foreach (var timeSlot in existingSessions)
        {
            if (Overlaps(timeSlot, TimeSpan.FromMinutes(10))) throw new PolicyException($"Diffrence between session must be more than 10 minutes");
        }

        bool Overlaps(SessionTimeSlot other, TimeSpan minGap)
        {
            bool intersects = start < other.end && end > other.start;

            if (intersects) return true;

            bool tooClose = start < other.end + minGap && end > other.start - minGap;

            return tooClose;
        }
    }
}
