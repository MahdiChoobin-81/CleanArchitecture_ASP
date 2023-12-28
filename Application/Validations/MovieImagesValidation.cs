using Application.Data;
using FluentResults;
using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Application.Validations;

public class MovieImagesValidation
{

    public static async Task<Result<List<MovieImage>>> ValidateIdAndName(List<MovieImageDto> movieImages, Id id, IMovieImageRepository movieImageRepository)
    {
        foreach (var image in movieImages)
        {
            var imageValidation = Image.Create(image.Image);
            if (imageValidation.IsFailed)
            {
                return imageValidation.ToResult();
            }
        }
        
        // var findMovieImages = await FindOrMakeMovieImage
        //     .FindOrMake(movieImages, id);

        var findMovieImages = await new FindOrMakeMovieImage(movieImageRepository)
            .FindOrMake(movieImages, id);
        
        if (findMovieImages is not null)
        {
            return Result.Ok(findMovieImages);
        }

        return Result.Ok();
        
    }
    
    public static Result ValidateName(List<string> movieImages)
    {
        foreach (var image in movieImages)
        {
            
            var imageValidation = Image.Create(image);
            if (imageValidation.IsFailed)
            {
                return imageValidation.ToResult();
            }
        }

        return Result.Ok();
    }
}