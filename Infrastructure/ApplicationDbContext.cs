using Application.Data;
using Domain.Entities;
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
        
        // modelBuilder.Entity<User>()
        //     .Property(u => u.Username)
        //     .HasConversion(
        //         userId => userId.Value,
        //         value => Username.Create(value)
        //     );
        
        
        modelBuilder.Entity<User>(u => u.ComplexProperty(p => p.CreatedAt));
        
        // modelBuilder.Entity<User>()
        //     .Property(u => u.CreatedAt)
        //     .HasConversion(
        //         CreatedAt => CreatedAt.Value,
        //         value => new CreatedAt(value)
        //     );


        // modelBuilder.Entity<User>().ComplexProperty(u => u.Email);


        // modelBuilder.Entity<User>(u =>
        // {
        //     u.ComplexProperty(p => p.CreatedAt);
        //     u.ComplexProperty(p => p.Email);
        //     u.ComplexProperty(p => p.Password);
        //     u.ComplexProperty(p => p.FullName);
        //     u.ComplexProperty(p => p.Username);
        // });


    }
    public DbSet<User> Users { get; set; }
    
}