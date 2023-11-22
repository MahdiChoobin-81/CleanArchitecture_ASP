using Application.Data;
using Domain.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
{ 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    optionsBuilder.UseSqlServer(
        @"Server=localhost\SQLEXPRESS;Database=Test;Trusted_Connection=True;TrustServerCertificate=True;");
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        modelBuilder.Entity<User>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<User>()
            .Property(u => u.Id)
            .HasConversion(
                userId => userId.Value,
                value => new UserId(value)
                );
        
        modelBuilder.Entity<User>()
            .Property(u => u.CreatedAt)
            .HasConversion(
                createdAt => createdAt.Value,
                value => new CreatedAt(value)
            );
        modelBuilder.Entity<User>()
            .Property(u => u.Username)
            .HasConversion(
                username => username.Value,
                value => null
                );
        modelBuilder.Entity<User>()
            .Property(u => u.Password)
            .HasConversion(
                password => password.Value,
                value => null
            );
        modelBuilder.Entity<User>()
            .Property(u => u.Email)
            .HasConversion(
                email => email.Value,
                value => null
            );
        modelBuilder.Entity<User>()
            .Property(u => u.FullName)
            .HasConversion(
                userFullName => userFullName.Value,
                value => null
            );


    }
    public DbSet<User> Users { get; set; }
    
}