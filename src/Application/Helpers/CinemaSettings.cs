using Cinema.Domain.Abstractions;

namespace Cinema.Application.Helpers;

public class CinemaSettings : ICinemaSettings
{
    public int OpeningHour { get; set; }
    public int ClosingHour { get; set; }
    public int MinMinuteAddSession { get; set; }
    public int MaxDaysAddSession { get; set; }
}
