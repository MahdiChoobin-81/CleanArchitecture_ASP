// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Movie_asp.Entities;
//
// namespace Infrastructure.Configurations;
//
// public class MovieCollectionConfiguration : IEntityTypeConfiguration<MovieCollection>
// {
//     public void Configure(EntityTypeBuilder<MovieCollection> builder)
//     {
//         builder.HasKey(mc => new { mc.MovieId,
//             mc.CountryId,
//             mc.GenreId,
//             mc.LanguageId,
//             mc.ActorId
//         });
//         
//         builder.HasOne<Movie>(mc => mc.Moive)
//             .WithMany(m => m.MovieCollection)
//             .HasForeignKey(mc => mc.MovieId);
//
//
//         builder.HasOne<Country>(mc => mc.Country)
//             .WithMany(c => c.MovieCollection)
//             .HasForeignKey(mc => mc.CountryId);
//         
//         builder.HasOne<Genre>(mc => mc.Genre)
//             .WithMany(g => g.MovieCollection)
//             .HasForeignKey(mc => mc.GenreId);
//
//         builder.HasOne<Language>(mc => mc.Language)
//             .WithMany(l => l.MovieCollections)
//             .HasForeignKey(mc => mc.LanguageId);
//
//         builder.HasOne<Actor>(mc => mc.Actor)
//             .WithMany(a => a.MovieCollections)
//             .HasForeignKey(mc => mc.ActorId);
//
//
//     }
// }