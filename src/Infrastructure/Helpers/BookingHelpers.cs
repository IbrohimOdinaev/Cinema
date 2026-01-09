using Cinema.Infrastructure.DbEntities;
using Cinema.Domain.Entities;

namespace Cinema.Infrastructure.Helpers;

public static class BookingHelper
{
    public static decimal CalculateBookingCost(this DbBooking booking)
    {
        if (booking.Session is null || booking.Session.Film is null) return -1;

        return booking.Session.Film.Price * booking.Seats.Count();
    }

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
            Status = booking.Status,
            Seats = booking.Seats.ToList()
        };
    }

    public static Booking ToDomain(this DbBooking dbBooking)
    {
        return new Booking
        {
            Id = dbBooking.Id,
            UserId = dbBooking.UserId,
            User = dbBooking.User?.ToDomain(),
            SessionId = dbBooking.SessionId,
            Session = dbBooking.Session?.ToDomain(),
            Cost = dbBooking.Cost,
            Status = dbBooking.Status,
            Seats = dbBooking.Seats.ToList()
        };
    }
}
