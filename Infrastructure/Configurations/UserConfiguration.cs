using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value)
            );
        
        builder.Property(u => u.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => new CreatedAt(value)
            );
        
        
        builder.Property(u => u.Username)
            .HasConversion(
                username => username.Value,
                value => Username.Create(value).Value!
            );
        
        builder.Property(u => u.Password)
            .HasConversion(
                password => password.Value,
                value => Password.Create(value).Value!
            );
        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value!
            );
        builder.Property(u => u.FullName)
            .HasConversion(
                userFullName => userFullName.Value,
                value => UserFullName.Create(value).Value!
            );

        builder.HasIndex(u => u.Email).IsUnique();

    }
}