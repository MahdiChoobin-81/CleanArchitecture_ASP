using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Genre;

namespace Infrastructure.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.HasKey(g => g.Id);
        builder.HasIndex(g => g.GenreName);

        builder.Property(g => g.Id)
            .HasConversion(
                genreId => genreId.Value,
                value => new Id(value)
                );

        builder.Property(g => g.GenreName)
            .HasConversion(
                genreName => genreName.Value,
                value => GenreName.Create(value).Value!
                );



    }
}