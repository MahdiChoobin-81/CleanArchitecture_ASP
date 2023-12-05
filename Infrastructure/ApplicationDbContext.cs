using Application.Data;
using Domain.Entities;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{ 
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    optionsBuilder.UseSqlServer(
        @"Server=localhost\SQLEXPRESS;Database=Test;Trusted_Connection=True;TrustServerCertificate=True;");
    
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

    }
    public DbSet<User> Users { get; set; }
    
}