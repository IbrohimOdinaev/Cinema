using Cinema.Infrastructure.DbEntities;
using Cinema.Domain.Entities;

namespace Cinema.Infrastructure.Helpers;

public static class BookingHelper
{
    public static DbBooking ToDb(this Booking booking)
    {
        return new DbBooking
        {
            Id = booking.Id,
            UserId = booking.UserId,
            User = booking.User?.ToDb(),
            SessionId = booking.SessionId,
            Session = booking.Session?.ToDb(),
            Cost = booking.Cost,
            Seats = booking.Seats.ToList()
        };
    }

    public static Booking ToDomain(this DbBooking dbBooking)
    {
        return new Booking
            (
                dbBooking.Id,
                dbBooking.UserId,
                dbBooking.User?.ToDomain(),
                dbBooking.SessionId,
                dbBooking.Session?.ToDomain(),
                dbBooking.Cost,
                dbBooking.Seats.ToList()
            );
    }
}
