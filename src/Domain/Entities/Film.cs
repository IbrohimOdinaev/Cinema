using Cinema.Domain.Exceptions;

namespace Cinema.Domain.Entities;

public class Film
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int Duration { get; private set; }

    public Film(string title, decimal price, int duration)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new DomainArgumentException("Title cannot be null or empty");

        if (price < 0)
            throw new DomainArgumentException("Price cannot be negative");

        if (duration < 0)
            throw new DomainArgumentException("Duration cannot be negative");
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        Duration = duration;
    }

    public Film(Guid id, string title, decimal price, int duration)
    {
        Id = id;
        Title = title;
        Price = price;
        Duration = duration;
    }

    public void ChangeTitle(string newTitle)
    {
        if (string.IsNullOrWhiteSpace(newTitle))
            throw new DomainArgumentException("Title cannot be null or empty.");

        Title = newTitle;
    }

    public void ChangePrice(decimal newPrice)
    {
        if (Price < 0)
            throw new DomainArgumentException("Price cannot be negative.");

        Price = newPrice;
    }

    public void ChangeDuration(int newDuration)
    {
        if (newDuration < 0)
            throw new DomainArgumentException("Duratin cannot be negative");

        Duration = newDuration;
    }
}
