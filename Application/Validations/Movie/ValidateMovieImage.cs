using Application.Dto.Entities;
using FluentResults;
using Movie_asp.ValueObjects;

namespace Application.Validations.Movie;

public static class ValidateMovieImage
{
    public static Result ValidateImage(this List<MovieImageDto> rowMovieImages)
    {
        foreach (var movieImage in rowMovieImages)
        {
            
            var imageValidation = Image.Create(movieImage.Image);
            if (imageValidation.IsFailed)
            {
                return imageValidation.ToResult();
            }
        }

        return Result.Ok();
    }
}