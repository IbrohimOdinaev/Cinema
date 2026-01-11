using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.Helpers;

public static class SeatHelpers
{
    public static DbSeat ToDb(this Seat seat)
    {
        return new DbSeat
        {
            Id = seat.Id,
            HallId = seat.HallId,
            Raw = seat.Raw,
            Num = seat.Num,
            IsOccupied = seat.IsOccupied
        };
    }

    public static Seat ToDomain(this DbSeat dbSeat)
    {
        return new Seat
        {
            Id = dbSeat.Id,
            HallId = dbSeat.HallId,
            Raw = dbSeat.Raw,
            Num = dbSeat.Num,
            IsOccupied = dbSeat.IsOccupied
        };
    }
}

