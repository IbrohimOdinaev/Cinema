using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.Helpers;

public static class HallHelpers
{
    public static DbHall ToDb(this Hall hall)
    {
        return new DbHall
        {
            Id = hall.Id,
            Title = hall.Title,
            Seats = hall.Seats.Select(s => s.ToDb()).ToList()
        };
    }

    public static Hall ToDomain(this DbHall dbHall)
    {
        return new Hall
        {
            Id = dbHall.Id,
            Title = dbHall.Title,
            Seats = dbHall.Seats.Select(s => s.ToDomain()).ToList()
        };
    }
}

