namespace Cinema.Application.Exceptions;

public class ApplicationPersistenceException : ApplicationException
{
    public ApplicationPersistenceException(string message)
      : base("Persistence Error", message)
    {

    }
}
