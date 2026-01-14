using Cinema.Domain.Entities;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure.Helpers;

public static class HallHelpers
{
    public static DbHall ToDb(this Hall hall, AppDbContext context)
    {
        return new DbHall
        {
            Id = hall.Id,
            Title = hall.Title,
            Seats = hall.Seats.Select(s => s.ToDb(context)).ToList()
        };
    }

    public static Hall ToDomain(this DbHall dbHall)
    {
        return new Hall
        (
            dbHall.Id,
            dbHall.Title,
            dbHall.Seats.Select(s => s.ToDomain()).ToList()
        );
    }
}

