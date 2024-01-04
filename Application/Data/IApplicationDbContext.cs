using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;

namespace Application.Data;

public interface IApplicationDbContext : IUnitOfWork
{
 
    public DbSet<User> Users { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Country> Countries { get; set; }
    
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Movie> Movies { get; set; }
    
    public DbSet<MovieImage> MovieImages { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}