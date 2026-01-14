namespace Cinema.Application.DTOS;

public record HallResponse
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;

    public HallResponse(Guid id, string title)
    {
        Id = id;
        Title = title;
    }

    public HallResponse() { }
}

