using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Movies.Query;

public record GetMovieByIdQuery(Id Id) : IRequest<CustomGenericResult>;