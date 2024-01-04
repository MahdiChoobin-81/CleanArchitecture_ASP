using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects.Movie;

namespace Movie_asp.Repositories;

public interface IMovieRepository : IRepository<Movie>
{
    Task<Result> Add(Movie movie);

    void Remove(Movie movie);

    Task<Result> Update(
        Movie movie,
        MovieName currentMovieName,
        List<MovieImage> currentMovieImages);

    Task<IEnumerable<Movie>> GetAllAsync();

}