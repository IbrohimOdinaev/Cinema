namespace Cinema.Domain.Abstractions;

public interface IClock
{
    DateTime Now { get; }
}
