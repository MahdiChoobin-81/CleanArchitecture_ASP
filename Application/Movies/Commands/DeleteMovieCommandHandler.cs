using Application.Data;
using Application.Dto.Results;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Repositories;

namespace Application.Movies.Commands;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, MovieResultDto>
{
    
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<MovieResultDto> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var movie = await _movieRepository.GetByIdAsync(request.id);
        if (movie is null)
        {
            return Result.Fail("There's no Movie with This Id : " + request.id)
                .ToMovieResultDto(null, StatusCode.NotFound);
        }
    
        _movieRepository.Remove(movie);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok().ToMovieResultDto(movie, StatusCode.Ok);

    }
}