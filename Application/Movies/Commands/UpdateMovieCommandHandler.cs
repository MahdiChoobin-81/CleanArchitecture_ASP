using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;

namespace Application.Movies.Commands;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, MovieResultDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IMovieImageRepository _movieImageRepository;
    
    

    public UpdateMovieCommandHandler(IUnitOfWork unitOfWork, IMovieRepository movieRepository, IGenreRepository genreRepository, IMovieImageRepository movieImageRepository)
    {
        _unitOfWork = unitOfWork;
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
        _movieImageRepository = movieImageRepository;
    }

    public async Task<MovieResultDto> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        // check if movie exists
        var movie = await _movieRepository.GetByIdAsync(request.Id);
        if(movie is null)
            return Result.Fail("There's no Movie with This Id : " + request.Id)
                .ToMovieResultDto(null, StatusCode.NotFound);
        
        var movieNameResult = MovieName.Create(request.UpdateMovieDto.MovieName);
        var movieDescriptionResult = MovieDescription.Create(request.UpdateMovieDto.MovieDescription);
        var movieRateResult = MovieRate.Create(request.UpdateMovieDto.MovieRate);
        var releaseDateResult    = ReleaseDate.Create(request.UpdateMovieDto.ReleaseDate);
        var mainImageResult    = Image.Create(request.UpdateMovieDto.MainImage);
        var subtitle    = request.UpdateMovieDto.Subtitle;
        List<MovieImageDto> movieImages = request.UpdateMovieDto.MovieImages;
        List<Guid> genreIds = request.UpdateMovieDto.GenreIds;
        List<Genre> genres = new List<Genre>();
        

        // check if genre's are exists
        foreach (var genreId in genreIds)
        {
            var genre = await _genreRepository.FindByIdAsync(new Id(genreId));
            
            if (genre is null){
                return Result.Fail("There's no Genre With This Id : " + genreId +
                                   " in Genre Table.").ToMovieResultDto(null, StatusCode.NotFound);
            }

            genres.Add(genre);
        }
        
        var validateMovie = MovieValidation.IsValid(
            movieNameResult,
            movieDescriptionResult,
            movieRateResult,
            releaseDateResult,
            mainImageResult);

        var validateMovieImages = await MovieImagesValidation
            .ValidateIdAndName(movieImages, request.Id, _movieImageRepository);
        
        if (validateMovie.IsFailed)
            return validateMovie.ToMovieResultDto(null, StatusCode.BadRequest);
        
        if (validateMovieImages.IsFailed)
            return validateMovieImages.ToResult().ToMovieResultDto(null, StatusCode.BadRequest);
        
        movie.MoiveImages = validateMovieImages.Value;
        movie.Genres = genres;
        
        
        movie.Update(
            movieNameResult.Value!,
            movieDescriptionResult.Value!,
            movieRateResult.Value!,
            releaseDateResult.Value!,
            mainImageResult.Value!,
            subtitle
            );
        
        _movieRepository.Update(movie);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok().ToMovieResultDto(movie, StatusCode.Ok);

    }
}