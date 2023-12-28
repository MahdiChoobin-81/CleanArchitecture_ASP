using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Application.Movies.Query;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, MovieResultDto>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<MovieResultDto> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.id);
        if (movie is null)
        {
            return Result.Fail("There's no Movie with This Id : " + request.id)
                .ToMovieResultDto(null, StatusCode.NotFound);
        }

        return Result.Ok().ToMovieResultDto(movie, StatusCode.Ok);
    }
}