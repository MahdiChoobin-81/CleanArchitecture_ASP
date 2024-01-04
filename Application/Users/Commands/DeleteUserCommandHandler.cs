using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using Application.Validations.User;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;

namespace Application.Users.Commands;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, CustomGenericResult>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomGenericResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var findUser = await FindEntityRecordById<User>.Find(
            _userRepository, request.Id);

        if (findUser.IsFailed)
        {
            return findUser.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        }
        var user = findUser.Value;

        _userRepository.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Ok().ToCustomGenericResult(user, StatusCode.Ok);
    }
}