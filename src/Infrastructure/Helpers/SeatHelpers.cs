using Cinema.Domain.Entities;
using Cinema.Domain.ValueObjects;
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
            Raw = seat.Position.Raw,
            Num = seat.Position.Num,
            IsOccupied = seat.IsOccupied
        };
    }

    public static Seat ToDomain(this DbSeat dbSeat)
    {
        return new Seat
        (
            dbSeat.Id,
            dbSeat.HallId,
            dbSeat.IsOccupied,
            Position.Create(dbSeat.Raw, dbSeat.Num)
        );
    }
}

