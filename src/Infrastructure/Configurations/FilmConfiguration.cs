using Cinema.Infrastructure.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

public class FilmConfiguration : IEntityTypeConfiguration<DbFilm>
{
    public void Configure(EntityTypeBuilder<DbFilm> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.Title)
          .IsRequired();

        builder.Property(f => f.Price)
          .IsRequired()
          .HasColumnType("decimal(18,2)");
    }
}
