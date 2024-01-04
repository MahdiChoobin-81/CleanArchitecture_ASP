using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Application.Movies.Query;

public class GetMovieByIdQueryHandler : IRequestHandler<GetMovieByIdQuery, CustomGenericResult>
{
    private readonly IMovieRepository _movieRepository;

    public GetMovieByIdQueryHandler(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }

    public async Task<CustomGenericResult> Handle(GetMovieByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await FindEntityRecordById<Movie>.Find(
            _movieRepository, request.Id);

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        }
        var movie = result.Value;

        return Result.Ok().ToCustomGenericResult(movie, StatusCode.Ok);
    }
}