namespace Cinema.Application.DTOS;

public record RawNum
{
    public int Raw { get; init; }
    public int Num { get; init; }

    public RawNum(int raw, int num)
    {
        Raw = raw;
        Num = num;
    }

    public RawNum() { }
}

