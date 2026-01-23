namespace Cinema.Domain.Exceptions;

public class BusinessRuleException : DomainException
{
    public BusinessRuleException(string message)
      : base("Business Rule Violation", message)
    { }
}
