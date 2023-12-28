using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;
using Country = Movie_asp.Entities.Country;

namespace Infrastructure.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Id)
            .HasConversion(
                id => id.Value,
                value => new Id(value)
                );

        builder.Property(m => m.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => new CreatedAt(value)
            );
        
        builder.Property(m => m.MovieName)
            .HasConversion(
                movieName => movieName.Value,
                value => MovieName.Create(value).Value!
                );
        
        builder.Property(m => m.MovieDescription)
            .HasConversion(
                movieDescription => movieDescription.Value,
                value => MovieDescription.Create(value).Value!
            );
        
        builder.Property(m => m.MovieRate)
            .HasConversion(
                movieRate => movieRate.Value,
                value => MovieRate.Create(value).Value!
            );
        
        builder.Property(m => m.ReleaseDate)
            .HasConversion(
                releaseDate => releaseDate.Value,
                value => ReleaseDate.Create(value).Value!
            );
        
        builder.Property(m => m.MainImage)
            .HasConversion(
                mainImage => mainImage.Value,
                value => Image.Create(value).Value!
            );

    }
}