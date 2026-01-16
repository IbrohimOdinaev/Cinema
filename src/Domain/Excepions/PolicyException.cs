namespace Cinema.Domain.Exceptions;

public class PolicyException : AppException
{
    public PolicyException(string message)
      : base("Policy Failed", message, 400)
    { }
}
