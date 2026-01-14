namespace Cinema.Application.DTOS;

public record SeatResponse
{
    public Guid Id { get; init; }
    public bool IsOccupied { get; init; }
    public int Raw { get; init; }
    public int Num { get; init; }

    public SeatResponse(Guid id, bool isOccupied, int raw, int num)
    {
        Id = id;
        IsOccupied = isOccupied;
        Raw = raw;
        Num = num;
    }

    public SeatResponse() { }

}
