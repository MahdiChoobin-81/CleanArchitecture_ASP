using FluentResults;
using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;

namespace Application.Validations;

public static class MovieValidation
{
    public static Result IsValid(
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