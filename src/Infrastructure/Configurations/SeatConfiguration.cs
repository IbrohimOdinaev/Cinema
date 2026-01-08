using Cinema.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class SeatConfiguration : IEntityTypeConfiguration<DbSeat>
{
    public void Configure(EntityTypeBuilder<DbSeat> builder)
    {
        builder.HasKey(s => s.Id);

        builder
          .Property(s => s.IsOccupied)
          .IsRequired();

        builder
          .Property(s => s.Raw)
          .IsRequired();

        builder
          .Property(s => s.Num)
          .IsRequired();
    }
}
