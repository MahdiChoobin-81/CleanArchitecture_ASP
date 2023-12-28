using System.Collections;
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

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, MovieResultDto>
{

    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    // private readonly IActorRepository _actorRepository;
    // private readonly ILanguageRepository _languageRepository;
    // private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AddMovieCommandHandler(
        IMovieRepository movieRepository,
        IUnitOfWork unitOfWork,
        IGenreRepository genreRepository
        // IActorRepository actorRepository,
        // ICountryRepository countryRepository,
        // ILanguageRepository languageRepository
        )
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
        _genreRepository = genreRepository;
        // _actorRepository = actorRepository;
        // _countryRepository = countryRepository;
        // _languageRepository = languageRepository;
    }

    public async Task<MovieResultDto> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        var movieId = new Id(Guid.NewGuid());
        var movieNameResult = MovieName.Create(request.MovieName);
        var movieDescriptionResult = MovieDescription.Create(request.MovieDescription);
        var movieRateResult = MovieRate.Create(request.MovieRate);
        var releaseDateResult    = ReleaseDate.Create(request.ReleaseDate);
        var mainImageResult    = Image.Create(request.MainImage);
        var subtitle    = request.Subtitle;
        var createdAt = new CreatedAt(DateTime.Now);
        List<string> images = request.Images;
        List<Guid> genreIds = request.GenresIds;
        
        List<Genre> Genres = new List<Genre>();

        var validateMovie = MovieValidation.IsValid(
            movieNameResult,
            movieDescriptionResult,
            movieRateResult,
            releaseDateResult,
            mainImageResult);

        var validateMovieImages =MovieImagesValidation.ValidateName(images);
        
        if (validateMovie.IsFailed)
            return validateMovie.ToMovieResultDto(null, StatusCode.BadRequest);
        
        if (validateMovieImages.IsFailed)
            return validateMovieImages.ToMovieResultDto(null, StatusCode.BadRequest);

        var movie = new Movie(
            movieId,
            movieNameResult.Value!,
            movieDescriptionResult.Value!,
            movieRateResult.Value!,
            releaseDateResult.Value!,
            mainImageResult.Value!,
            subtitle,
            createdAt
        );

        var movieImages = images.Select(
            img => new MovieImage(new Id(Guid.NewGuid()),
                Image.Create(img).Value!)).ToList();

        foreach (var movieImage in movieImages)
        {
            movieImage.MovieId = movie.Id;
        }

        
        foreach (var genreId in genreIds)
        {
            var findGenre = await _genreRepository.FindByIdAsync(new Id(genreId));
            
            if (findGenre is null){
                return Result.Fail("There's no Genre With This Id : " + genreId +
                                   " in Genre Table.").ToMovieResultDto(null, StatusCode.NotFound);
            }

            Genres.Add(findGenre);
        }
        
        
        foreach (var genre in Genres)
        {
            genre.Movies = new List<Movie> {movie}.ToList();
        }
        
        movie.MoiveImages = movieImages;
        movie.Genres = Genres;
        
        _movieRepository.Add(movie);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return validateMovie.ToMovieResultDto(movie, StatusCode.Created);

    }
}


// foreach (Genre genre in genres)
// {
//     bool isFound = await _genreRepository.FindByIdAsync(genre.Id);
//     if (!isFound){
//         return Result.Fail("There's no Genre With This Name : " + genre.GenreName +
//                            " Id : " + genre.Id).ToMovieResultDto(null);
//     }
// }

// Result validateGenres = await MovieListsValidation<Genre>
//     .Find(genres, _genreRepository);
// if (validateGenres.IsFailed)
//     return validateGenres.ToMovieResultDto(null);
//
// Result validateActors = await MovieListsValidation<Actor>
//     .Find(actors, _actorRepository);
// if (validateActors.IsFailed)
//     return validateActors.ToMovieResultDto(null);
//
// Result validateLanguages = await MovieListsValidation<Language>
//     .Find(languages, _languageRepository);
// if (validateLanguages.IsFailed)
//     return validateLanguages.ToMovieResultDto(null);
//
// Result validateCountries = await MovieListsValidation<Country>
//     .Find(countries, _countryRepository);
// if (validateCountries.IsFailed)
//     return validateCountries.ToMovieResultDto(null);


// List<MoiveImage> movieImages = null;
//
// foreach (string image in images)
// {
//     var validationImage = Image.Create(image);
//     if (validationImage.IsFailed)
//         return validationImage.ToResult().ToMovieResultDto(null);
//     
//     var movieImageId = new Id(Guid.NewGuid());
//     var movieImage = validationImage;
//     movieImages.Add(new MoiveImage(movieImageId, movieImage.Value!));
// }