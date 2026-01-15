using Cinema.Infrastructure.DbEntities;
using Cinema.Domain.Entities;

namespace Cinema.Infrastructure.Helpers;

public static class BookingHelper
{
    public static DbBooking ToDb(this Booking booking, AppDbContext context)
    {
        return new DbBooking
        {
            Id = booking.Id,
            UserId = booking.UserId,
            User = booking.User?.ToDb(context),
            SessionId = booking.SessionId,
            Session = booking.Session?.ToDb(context),
            Cost = booking.Cost,
            Seats = booking.Seats
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
                dbBooking.Seats
            );
    }
}
