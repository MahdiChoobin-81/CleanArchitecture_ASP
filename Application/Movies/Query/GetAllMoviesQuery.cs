using MediatR;
using Movie_asp.Entities;

namespace Application.Movies.Query;

public record GetAllMoviesQuery() : IRequest<IEnumerable<Movie>>;