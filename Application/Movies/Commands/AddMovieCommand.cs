
using Application.Dto.Entities;
using Application.Dto.Results;
using MediatR;

namespace Application.Movies.Commands;

public record AddMovieCommand(
    string MovieName,
    string MovieDescription,
    sbyte MovieRate,
    DateTime ReleaseDate,
    string MainImage,
    bool Subtitle,
    List<MovieImageDto> MovieImages,
    List<Guid> GenresIds,
    List<Guid> LanguagesIds,
    List<Guid> CountriesIds,
    List<Guid> ActorsIds
    ) : IRequest<CustomGenericResult>;