using Application.Data;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly IApplicationDbContext _context;

    public GenreRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Genre?> FindByIdAsync(Id id)
    {
        var result = await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
        return result;
    }
    
    

}