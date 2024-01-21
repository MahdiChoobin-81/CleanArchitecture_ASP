using Application.Data;
using Application.Dto.Entities;
using Application.Dto.Results;
using Application.Validations;
using Application.Validations.Movie;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;

namespace Application.Movies.Commands;

public class UpdateMovieCommandHandler : IRequestHandler<UpdateMovieCommand, CustomGenericResult>
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IActorRepository _actorRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    

    public UpdateMovieCommandHandler(IUnitOfWork unitOfWork, IMovieRepository movieRepository, IGenreRepository genreRepository,IActorRepository actorRepository, ILanguageRepository languageRepository, ICountryRepository countryRepository)
    {
        _unitOfWork = unitOfWork;
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
        _actorRepository = actorRepository;
        _languageRepository = languageRepository;
        _countryRepository = countryRepository;
    }

    public async Task<CustomGenericResult> Handle(UpdateMovieCommand request, CancellationToken cancellationToken)
    {
        
        // find Movie by id
        var findMovie = await FindEntityRecordById<Movie>.Find(
            _movieRepository, request.Id);

        if (findMovie.IsFailed)
        {
            return findMovie.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        }

        var movie = findMovie.Value;
        var currentMovieName = movie.MovieName;
        var currentMovieImages = movie.MoiveImages;
        
        
        var movieName = MovieName.Create(request.MovieDto.MovieName);
        var movieDescription = MovieDescription.Create(request.MovieDto.MovieDescription);
        var movieRate = MovieRate.Create(request.MovieDto.MovieRate);
        var releaseDate    = ReleaseDate.Create(request.MovieDto.ReleaseDate);
        var mainImage    = Image.Create(request.MovieDto.MainImage);
        var subtitle    = request.MovieDto.Subtitle;
        
        
        List<MovieImageDto> movieImageDtoList = request.MovieDto.MovieImages;
        
        // validate values for update movie
        var movieValidation = ValidateMovie.Validate(
            movieName,
            movieDescription,
            movieRate,
            releaseDate,
            mainImage
        );
        if (movieValidation.IsFailed)
            return movieValidation.ToCustomGenericResult(null, StatusCode.BadRequest);
        
        
        // find genres
        var findGenres = await FindDependentTableRecordsOfMovieTable<Genre>
            .Find(request.MovieDto.GenreIds, _genreRepository);
            
        if (findGenres.IsFailed)
            return findGenres.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        
        // find Languages
        var findLanguages = await FindDependentTableRecordsOfMovieTable<Language>
            .Find(request.MovieDto.LanguagesIds, _languageRepository);

        if (findLanguages.IsFailed)
            return findLanguages.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        
        // find Countries
        var findCountries = await FindDependentTableRecordsOfMovieTable<Country>
            .Find(request.MovieDto.CountriesIds, _countryRepository);

        if (findCountries.IsFailed)
            return findCountries.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        
        // find Actors
        var findActors = await FindDependentTableRecordsOfMovieTable<Actor>
            .Find(request.MovieDto.ActorsIds, _actorRepository);

        if (findActors.IsFailed)
            return findActors.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);

        
        
        // create movie images.
        var createMoveImagesResult = movieImageDtoList.CreateImageInstances();
        
        if(createMoveImagesResult.IsFailed)
            return createMoveImagesResult.ToResult().ToCustomGenericResult(null, StatusCode.BadRequest);
        
        
        var genres = findGenres.Value;
        var actors = findActors.Value;
        var countries = findCountries.Value;
        var languages = findLanguages.Value;
        var movieImages = createMoveImagesResult.Value;

        movie.MoiveImages = movieImages;
        movie.Genres = genres;
        movie.Languages = languages;
        movie.Countries = countries;
        movie.Actors = actors;

        
        movie.Update(
            movieName.Value,
            movieDescription.Value,
            movieRate.Value,
            releaseDate.Value,
            mainImage.Value,
            subtitle
            );
        
        var result = await _movieRepository.Update(movie, currentMovieName, currentMovieImages);
        
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, StatusCode.BadRequest);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok().ToCustomGenericResult(movie, StatusCode.Ok);

    }
}