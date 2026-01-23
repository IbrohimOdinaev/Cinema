using Cinema.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<DbBooking>
{
    public void Configure(EntityTypeBuilder<DbBooking> builder)
    {
        builder.HasKey(b => b.Id);

        builder
          .HasOne(b => b.Session)
          .WithMany()
          .HasForeignKey(b => b.SessionId);

        builder
          .HasOne(b => b.User)
          .WithMany(u => u.Bookings)
          .HasForeignKey(b => b.UserId)
          .OnDelete(DeleteBehavior.Cascade);

        builder
          .Property(b => b.Cost)
          .HasColumnType("decimal(18,2)");
    }
}
