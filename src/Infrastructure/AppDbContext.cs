using Microsoft.EntityFrameworkCore;
using Cinema.Infrastructure.DbEntities;

namespace Cinema.Infrastructure;

public class AppDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
      : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<DbUser> Users => Set<DbUser>();
    public DbSet<DbBooking> Bookings => Set<DbBooking>();
    public DbSet<DbFilm> Films => Set<DbFilm>();
    public DbSet<DbSeat> Seats => Set<DbSeat>();
    public DbSet<DbSession> Sessions => Set<DbSession>();
    public DbSet<DbHall> Halls => Set<DbHall>();


}
