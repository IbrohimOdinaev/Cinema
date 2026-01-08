using Cinema.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<DbSession>
{
    public void Configure(EntityTypeBuilder<DbSession> builder)
    {
        builder.HasKey(s => s.Id);

        builder
          .Property(s => s.Start)
          .IsRequired()
          .HasConversion(
              t => t.ToTimeSpan(),
              t => TimeOnly.FromTimeSpan(t)
              );

        builder
          .Property(s => s.End)
          .IsRequired()
          .HasConversion(
              t => t.ToTimeSpan(),
              t => TimeOnly.FromTimeSpan(t)
              );

        builder
          .HasMany(s => s.Bookings)
          .WithOne(b => b.Session)
          .HasForeignKey(b => b.SessionId);

        builder
          .HasOne(s => s.Hall)
          .WithMany()
          .HasForeignKey(s => s.HallId)
          .OnDelete(DeleteBehavior.Cascade);

        builder
          .HasOne(s => s.Film)
          .WithMany()
          .HasForeignKey(s => s.FilmId)
          .OnDelete(DeleteBehavior.Cascade);
    }
}
