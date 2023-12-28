using Application.Data;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{

    private readonly IApplicationDbContext _context;

    public MovieRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(Movie movie)
    {
        _context.Movies.Add(movie);
    }

    public void Remove(Movie movie)
    {
        _context.Movies.Remove(movie);
    }

    public void Update(Movie movie)
    {
        _context.Movies.Update(movie);
    }

    public Task<Movie?> GetByIdAsync(Id id)
    {
        return _context.Movies.Include(m => m.Genres)
            .Include(m => m.MoiveImages)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _context.Movies.Include(m => m.Genres)
            .Include(m => m.MoiveImages)
            .ToListAsync();
    }
}