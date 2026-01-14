namespace Cinema.Domain.Entities;

public class Film
{
    public Guid Id { get; private set; }

    public string Title { get; private set; } = string.Empty;

    public decimal Price { get; private set; }

    public int Duration { get; private set; }

    public Film(string title, decimal price, int duration)
    {
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

    public void ChangeTitle(string newTitle) => Title = newTitle;

    public void ChangePrice(decimal newPrice) => Price = newPrice;

    public void ChangeDuration(int newDuration) => Duration = newDuration;
}
