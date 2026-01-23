namespace Cinema.Domain.Exceptions;

public class DomainArgumentException : DomainException
{
    public DomainArgumentException(string message)
      : base("Invalid argument", message)
    { }
}
