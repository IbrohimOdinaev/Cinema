namespace Cinema.Infrastructure.Exceptions;

public class InfrastructureException : Exception
{
    public string Title { get; }

    public InfrastructureException(string title, string message)
      : base(message)
    {
        Title = title;
    }
}
