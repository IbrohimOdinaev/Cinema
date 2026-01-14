namespace Cinema.Domain.ValueObjects;

public sealed class Duration
{
  public DateTime Start {get; }
  public DateTime End {get; }

  private Duration(DateTime start, int filmDuration)
  {
    Start = start;
    End = start.AddMinutes(filmDuration);
  }

  public static Duration Create(DateTime start, int filmDuration)
  {
    if (start < DateTime.UtcNow)
      throw new ArgumentException();

    if (filmDuration <= 0)
      throw new ArgumentException();

    return new (start, filmDuration);
  }
}
