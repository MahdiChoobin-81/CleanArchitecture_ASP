using Application.Data;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;

namespace Infrastructure;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{ 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    optionsBuilder.UseSqlServer(
        @"Server=localhost\SQLEXPRESS;Database=Test2;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        
        
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Country> Countries { get; set; }
    
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<MovieImage> MovieImages { get; set; }
    
}