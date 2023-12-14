using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Infrastructure.Configurations;

public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CountryName)
            .HasConversion(
                countryName => countryName.Value,
                value => CountryName.Create(value).Value!
                );

        builder.Property(c => c.Id)
            .HasConversion(
                id => id.Value,
                value => new Id(value)
                );


    }
}