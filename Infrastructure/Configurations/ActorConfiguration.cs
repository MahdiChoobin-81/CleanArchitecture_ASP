// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Metadata.Builders;
// using Movie_asp.Entities;
// using Movie_asp.ValueObjects;
//
// namespace Infrastructure.Configurations;
//
// public class ActorConfiguration : IEntityTypeConfiguration<Actor>
// {
//     public void Configure(EntityTypeBuilder<Actor> builder)
//     {
//         builder.HasKey(a => a.Id);
//
//
//         builder.Property(a => a.Id)
//             .HasConversion(
//                 actorId => actorId.Value,
//                 value => new Id(value)
//                 );
//
//         builder.Property(a => a.ActorName)
//             .HasConversion(
//                     fullName => fullName.Value,
//                     value => FullName.Create(value).Value!
//                 );
//
//         builder.Property(a => a.Img)
//             .HasConversion(
//                 image => image.Value,
//                 value => Image.Create(value).Value!
//                 );
//
//     }
// }