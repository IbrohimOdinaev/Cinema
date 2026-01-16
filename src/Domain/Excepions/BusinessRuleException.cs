namespace Cinema.Domain.Exceptions;

public class BusinessRuleException : AppException
{
    public BusinessRuleException(string message)
      : base("Business Rule Violation", message, 400)
    { }
}
