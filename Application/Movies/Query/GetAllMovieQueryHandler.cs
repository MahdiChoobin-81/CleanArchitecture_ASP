using Application.Data;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.Repositories;

namespace Application.Movies.Query;

public class GetAllMovieQueryHandler : IRequestHandler<GetAllMoviesQuery, IEnumerable<Movie>>
{
    private readonly IMovieRepository _movieRepository;

    public GetAllMovieQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<IEnumerable<Movie>> Handle(GetAllMoviesQuery request, CancellationToken cancellationToken)
    {
        return await _movieRepository.GetAllAsync();
    }
}