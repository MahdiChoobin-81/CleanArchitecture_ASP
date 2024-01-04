using Application.Validations.Movie;
using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;

namespace Application.Movies;

public class CreateMovieInstance
{
    public static Result<Movie?> Create(
        string rowMovieName,
        string rowMovieDescription,
        sbyte rowMovieRate,
        DateTime rowReleaseDate,
        string rowMainImage,
        bool rowSubtitle)
    {
        
        var movieName = MovieName.Create(rowMovieName);
        var movieDescription = MovieDescription.Create(rowMovieDescription);
        var movieRate = MovieRate.Create(rowMovieRate);
        var releaseDate    = ReleaseDate.Create(rowReleaseDate);
        var mainImage    = Image.Create(rowMainImage);
        var subtitle    = rowSubtitle;

        var movieValidation = ValidateMovie.Validate(
            movieName,
            movieDescription,
            movieRate,
            releaseDate,
            mainImage
            );

        if (movieValidation.IsFailed)
            return movieValidation;

        var movieId = new Id(Guid.NewGuid());
        var createdAt = new CreatedAt(DateTime.Now);
        
        var movie = new Movie(
            movieId,
            movieName.Value,
            movieDescription.Value,
            movieRate.Value,
            releaseDate.Value,
            mainImage.Value,
            subtitle,
            createdAt
        );
        
        return Result.Ok(movie);
        
    }
}