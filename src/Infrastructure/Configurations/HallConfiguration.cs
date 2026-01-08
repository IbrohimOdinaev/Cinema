using Cinema.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class HallConfiguration : IEntityTypeConfiguration<DbHall>
{
    public void Configure(EntityTypeBuilder<DbHall> builder)
    {
        builder.HasKey(h => h.Id);

        builder
          .Property(h => h.Title)
          .IsRequired();

        builder
          .HasMany(h => h.Seats)
          .WithOne()
          .HasForeignKey(s => s.HallId);
    }
}
