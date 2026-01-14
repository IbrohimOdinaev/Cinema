namespace Cinema.Application.DTOS;

public record CreateHallRequest
{
    public string Title { get; init; } = string.Empty;
    public int Raw { get; init; }
    public int Column { get; init; }

    public CreateHallRequest(string title, int raw, int column)
    {
        Title = title;
        Raw = raw;
        Column = column;
    }

    public CreateHallRequest() { }
}

