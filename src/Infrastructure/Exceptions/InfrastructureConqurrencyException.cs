namespace Cinema.Infrastructure.Exceptions;

public class InfrastructureConqurrencyException : InfrastructureException
{
    public InfrastructureConqurrencyException(string message)
      : base("Entity was modified concurrently", message)
    {

    }
}
