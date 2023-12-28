using Application.Dto.Results;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Movies.Commands;

public record DeleteMovieCommand(Id id) : IRequest<MovieResultDto>;