using Application.Dto.Entities;
using Application.Dto.Results;
using MediatR;
using Movie_asp.ValueObjects;

namespace Application.Movies.Commands;

public record UpdateMovieCommand(Id Id, MovieDto MovieDto) : IRequest<CustomGenericResult>;