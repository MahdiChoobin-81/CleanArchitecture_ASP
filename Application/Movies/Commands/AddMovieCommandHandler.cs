
using Application.Data;
using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;

namespace Application.Movies.Commands;

public class AddMovieCommandHandler : IRequestHandler<AddMovieCommand, CustomGenericResult>
{

    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    private readonly IActorRepository _actorRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;


    public AddMovieCommandHandler(
        IMovieRepository movieRepository,
        IUnitOfWork unitOfWork,
        IGenreRepository genreRepository,
        IActorRepository actorRepository,
        ICountryRepository countryRepository,
        ILanguageRepository languageRepository
        )
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
        _genreRepository = genreRepository;
        _actorRepository = actorRepository;
        _countryRepository = countryRepository;
        _languageRepository = languageRepository;
    }

    public async Task<CustomGenericResult> Handle(AddMovieCommand request, CancellationToken cancellationToken)
    {
        var createMovieResult = CreateMovieInstance.Create(
            request.MovieName,
            request.MovieDescription,
            request.MovieRate,
            request.ReleaseDate,
            request.MainImage,
            request.Subtitle);
        
        if (createMovieResult.IsFailed)
            return createMovieResult.ToResult().ToCustomGenericResult(null, StatusCode.BadRequest);

        var createMoveImagesResult = request.MovieImages.CreateImageInstances();
        
        if(createMoveImagesResult.IsFailed)
            return createMoveImagesResult.ToResult().ToCustomGenericResult(null, StatusCode.BadRequest);

        
        // TODO: Find a better way to find dependent table records ids of movie table.(make a class for it)

        // find Genres
        var findGenres = await FindDependentTableRecordsOfMovieTable<Genre>
            .Find(request.GenresIds, _genreRepository);

        if (findGenres.IsFailed)
            return findGenres.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        
        // find Languages
        var findLanguages = await FindDependentTableRecordsOfMovieTable<Language>
            .Find(request.LanguagesIds, _languageRepository);

        if (findLanguages.IsFailed)
            return findLanguages.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        
        // find Countries
        var findCountries = await FindDependentTableRecordsOfMovieTable<Country>
            .Find(request.CountriesIds, _countryRepository);

        if (findCountries.IsFailed)
            return findCountries.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        
        // find Actors
        var findActors = await FindDependentTableRecordsOfMovieTable<Actor>
            .Find(request.ActorsIds, _actorRepository);

        if (findActors.IsFailed)
            return findActors.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        
       
        var movie = createMovieResult.Value;
        var movieImages = createMoveImagesResult.Value;
        var genres = findGenres.Value;
        var actors = findActors.Value;
        var countries = findCountries.Value;
        var languages = findLanguages.Value;

        foreach (var movieImage in movieImages)
        {
            movieImage.MovieId = movie.Id;
        }
        
        // TODO: find a better way to add dependent tables to movie table.(make a class for it)
        movie.MoiveImages = movieImages;
        movie.Genres = genres;
        movie.Languages = languages;
        movie.Countries = countries;
        movie.Actors = actors;
        
        var result = await _movieRepository.Add(movie);
        
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, StatusCode.BadRequest);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok().ToCustomGenericResult(movie, StatusCode.Created);
    }
}