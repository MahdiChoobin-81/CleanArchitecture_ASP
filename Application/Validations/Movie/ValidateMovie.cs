using FluentResults;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;
using Movie_asp.ValueObjects.User;

namespace Application.Validations.Movie;

public static class ValidateMovie
{
    public static Result Validate(
        Result<MovieName> movieName,
        Result<MovieDescription> movieDescription,
        Result<MovieRate> movieRate,
        Result<ReleaseDate> releaseDate,
        Result<Image> mainImage)
    {
        if (movieName.IsFailed)
        {
            return movieName.ToResult();
        }
        if (movieDescription.IsFailed)
        {
            return movieDescription.ToResult();
        }
        if (releaseDate.IsFailed)
        {
            return releaseDate.ToResult();
        }
        if (movieRate.IsFailed)
        {
            return movieRate.ToResult();
        }
        if (mainImage.IsFailed)
        {
            return mainImage.ToResult();
        }
        

        return Result.Ok();
        
        
        
    }
}