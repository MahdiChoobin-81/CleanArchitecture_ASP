using System.Collections;
using Application.Dto.Results;
using MediatR;
using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Movies.Commands;

public record AddMovieCommand(
    string MovieName,
    string MovieDescription,
    sbyte MovieRate,
    DateTime ReleaseDate,
    string MainImage,
    bool Subtitle,
    List<string> Images,
    List<Guid> GenresIds
    ) : IRequest<MovieResultDto>;