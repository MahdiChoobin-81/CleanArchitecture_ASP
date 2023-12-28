using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Infrastructure.Configurations;

public class MovieImageConfiguration : IEntityTypeConfiguration<MovieImage>
{
    public void Configure(EntityTypeBuilder<MovieImage> builder)
    {

        builder.HasKey(mi => mi.Id);
        
        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => new Id(value)
            );
        
        builder.HasOne<Movie>(s => s.Movie)
            .WithMany(g => g.MoiveImages)
            .HasForeignKey(s => s.MovieId);

        builder.Property(m => m.Image)
            .HasConversion(
                image => image.Value,
                value => Image.Create(value).Value!
            );

    }
}



