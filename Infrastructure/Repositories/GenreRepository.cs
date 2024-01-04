using Application.Data;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Genre;

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
        return await _context.Genres.FirstOrDefaultAsync(g => g.Id == id);
    }


    public async Task<Result> Add(Genre genre)
    {
        IQueryable<Genre> genres = _context.Genres.AsQueryable();

        var isGenreNameUnique = await genres
            .Where(g => g.GenreName == genre.GenreName).FirstOrDefaultAsync();
        
        if (isGenreNameUnique is not null)
        {
            return Result.Fail("Genre already exist.");
        }
        _context.Genres.Add(genre);
        return Result.Ok();
    }

    public void Remove(Genre genre)
    {
        _context.Genres.Remove(genre);
    }

    public async Task<Result> Update(Genre genre, GenreName currentGenreName)
    {
        IQueryable<Genre> genres = _context.Genres.AsQueryable();

        var isGenreNameUnique = await genres
            .Where(g =>
                g.GenreName != currentGenreName &&
                g.GenreName == genre.GenreName)
            .FirstOrDefaultAsync();
            
        if (isGenreNameUnique is not null)
        {
            return Result.Fail("Genre already exist.");
        }
        
        _context.Genres.Update(genre);
        return Result.Ok();
    }

    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _context.Genres.ToListAsync();
    }
}