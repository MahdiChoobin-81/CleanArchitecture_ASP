using Application.Data;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;

namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{

    private readonly IApplicationDbContext _context;

    public MovieRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Add(Movie movie)
    {
        IQueryable<Movie> movies = _context.Movies.AsQueryable();
        IQueryable<MovieImage> movieImages = _context.MovieImages.AsQueryable();

        var isMovieNameUnique = await movies
            .Where(m => m.MovieName == movie.MovieName).FirstOrDefaultAsync();
        
        if (isMovieNameUnique is not null)
        {
            return Result.Fail("Movie Name already exist.");
        }

        foreach (var movieImage in movie.MoiveImages)
        {
            var isMovieImageIdUnique = await movieImages
                .Where(mi => mi.Id == movieImage.Id).FirstOrDefaultAsync();
            
            if (isMovieImageIdUnique is not null)
            {
                return Result.Fail("Id of Movie Image("+ movieImage.Image.Value +") already exist.");
            }
        }
        
        _context.Movies.Add(movie);
        return Result.Ok();
    }

    public void Remove(Movie movie)
    {
        _context.Movies.Remove(movie);
    }

    public async Task<Result> Update(
        Movie movie,
        MovieName currentMovieName,
        List<MovieImage> currentMovieImages)
    {
        IQueryable<Movie> movies = _context.Movies.AsQueryable();
        IQueryable<MovieImage> movieImages = _context.MovieImages.AsQueryable();

        var isMovieNameUnique = await movies
            .Where(u =>
                u.MovieName != currentMovieName &&
                u.MovieName == movie.MovieName)
            .FirstOrDefaultAsync();
            
        if (isMovieNameUnique is not null)
        {
            return Result.Fail("Movie Name already exist.");
        }
        
        // foreach (var movieImage in movie.MoiveImages)
        // {
        //     foreach (var currentMovieImage in currentMovieImages)
        //     {
        //         var isMovieImageIdUnique = await movieImages
        //             .Where(mi =>
        //                 mi.Id != currentMovieImage.Id &&
        //                 mi.Id == movieImage.Id)
        //             .FirstOrDefaultAsync();
        //     
        //         if (isMovieImageIdUnique is not null)
        //         {
        //             return Result.Fail("Id of Movie Image ("+ movieImage.Image.Value +") already exist.");
        //         }
        //     }
        //     
        // }
        
        
        _context.Movies.Update(movie);
        return Result.Ok();
    }


    public async Task<IEnumerable<Movie>> GetAllAsync()
    {
        return await _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.MoiveImages)
            .Include(m => m.Countries)
            .Include(m => m.Languages)
            .Include(m => m.Actors)
            .ToListAsync();
    }

    public Task<Movie?> FindByIdAsync(Id id)
    {
        return _context.Movies
            .Include(m => m.Genres)
            .Include(m => m.MoiveImages)
            .Include(m => m.Countries)
            .Include(m => m.Languages)
            .Include(m => m.Actors)
            .FirstOrDefaultAsync(m => m.Id == id);
    }
}