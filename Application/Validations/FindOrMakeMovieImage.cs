using Application.Data;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Application.Validations;

public class FindOrMakeMovieImage
{
    public List<string> Images = new ();
    public List<MovieImage> NewMovieImage = new ();
    
    private readonly IMovieImageRepository _movieImageRepository;

    public FindOrMakeMovieImage(IMovieImageRepository movieImageRepository)
    {
        _movieImageRepository = movieImageRepository;
    }


    public async Task<List<MovieImage>?> FindOrMake(List<MovieImageDto> movieImageDtos, Id movieId)
    {
        foreach (var movieImageDto in movieImageDtos)
        {
            Images.Add(movieImageDto.Image);
        }
        
        foreach (var movieImageDto in movieImageDtos)
        {

            var findImage = await _movieImageRepository.FindByIdNameMovieId(
                new Id(movieImageDto.Id),
                Image.Create(movieImageDto.Image).Value!,
                movieId);
            
            
            if (findImage is null)
            {
                var newMovieImage = new MovieImage(new Id(movieImageDto.Id),
                    Image.Create(movieImageDto.Image).Value!);
                
                newMovieImage.MovieId = movieId;
                NewMovieImage.Add(newMovieImage);
                
            }else
            {
               NewMovieImage.Add(findImage);
            }
        }
        return NewMovieImage;
    }
    
}