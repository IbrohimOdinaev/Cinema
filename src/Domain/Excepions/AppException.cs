namespace Cinema.Domain.Exceptions;

public abstract class AppException : Exception
{
    public string Title { get; }
    public int StatusCode { get; }

    protected AppException(string title, string message, int statusCode) : base(message)
    {
        Title = title;
        StatusCode = statusCode;
    }
}
