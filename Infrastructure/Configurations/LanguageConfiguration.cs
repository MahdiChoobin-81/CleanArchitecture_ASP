using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Language;

namespace Infrastructure.Configurations;

public class LanguageConfiguration : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasConversion(
                userId => userId.Value,
                value => new Id(value)
                );

        builder.Property(l => l.LanguageName)
            .HasConversion(
                language => language.Value,
                value => LanguageName.Create(value).Value!
                );
    }
}