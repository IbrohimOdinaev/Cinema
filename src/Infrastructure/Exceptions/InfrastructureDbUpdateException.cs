namespace Cinema.Infrastructure.Exceptions;

public class InfrastructureDbUpdateException : InfrastructureException
{
    public InfrastructureDbUpdateException(string message)
      : base("Failed to save entity", message)
    {
    }

}
