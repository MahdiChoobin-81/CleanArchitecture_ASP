using Application.Dto.Entities;
using Application.Validations.Movie;
using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Movies;

public static class CreateMovieImageInstance
{
    public static Result<List<MovieImage>> CreateImageInstances(this List<MovieImageDto> rowMovieImages)
    {
        
        var movieImagesValidation = rowMovieImages.ValidateImage();

        if (movieImagesValidation.IsFailed)
            return movieImagesValidation;
        
        var movieImages = rowMovieImages.Select(
            img => new MovieImage(
                new Id(img.Id),
                Image.Create(img.Image).Value))
            .ToList();

        return Result.Ok(movieImages);
    }
}