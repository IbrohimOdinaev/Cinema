namespace Cinema.Domain.Abstractions;

public interface ICinemaSettings
{
    int OpeningHour { get; set; }
    int ClosingHour { get; set; }
    int MinMinuteAddSession { get; set; }
    int MaxDaysAddSession { get; set; }
}
