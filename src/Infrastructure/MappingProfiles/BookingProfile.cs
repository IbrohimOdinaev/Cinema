using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.MappingProfiles;

public class BookingProfile : BaseProfile<Booking, DbBooking>;
