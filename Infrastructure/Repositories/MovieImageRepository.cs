using Application.Data;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Infrastructure.Repositories;

public class MovieImageRepository : IMovieImageRepository
{
    private readonly IApplicationDbContext _context;

    public MovieImageRepository(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<MovieImage?> FindByIdNameMovieId(Id id, Image image, Id movieId)
    {
        return await _context.MovieImages
            .Where(mi => 
                mi.Id == id && 
                mi.Image == image 
                && mi.MovieId == movieId
                )
            .FirstOrDefaultAsync();
        
    }
}