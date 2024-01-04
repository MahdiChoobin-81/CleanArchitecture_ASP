using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => Email.Create(value).Value!
            );
        
        builder.HasKey(i => i.Id);

        builder.Property(u => u.Id)
            .HasConversion(
                userId => userId.Value,
                value => new Id(value)
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
        
        builder.Property(u => u.FullName)
            .HasConversion(
                userFullName => userFullName.Value,
                value => FullName.Create(value).Value!
            );

        builder.HasIndex(u => u.Email).IsUnique();
        builder.HasIndex(u => u.Username).IsUnique();

    }
}