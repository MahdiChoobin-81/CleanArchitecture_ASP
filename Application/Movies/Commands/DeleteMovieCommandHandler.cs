using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;

namespace Application.Movies.Commands;

public class DeleteMovieCommandHandler : IRequestHandler<DeleteMovieCommand, CustomGenericResult>
{
    
    private readonly IMovieRepository _movieRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteMovieCommandHandler(IMovieRepository movieRepository, IUnitOfWork unitOfWork)
    {
        _movieRepository = movieRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CustomGenericResult> Handle(DeleteMovieCommand request, CancellationToken cancellationToken)
    {
        var findUser = await FindEntityRecordById<Movie>.Find(
            _movieRepository, request.Id);

        if (findUser.IsFailed)
        {
            return findUser.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        }
        
        var movie = findUser.Value;
    
        _movieRepository.Remove(movie);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok().ToCustomGenericResult(movie, StatusCode.Ok);

    }
}