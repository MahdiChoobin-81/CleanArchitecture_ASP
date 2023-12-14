using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using MediatR;
using Movie_asp.Repositories;

namespace Application.Users.Commands;

internal sealed class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, UserResultDto>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResultDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {

        var findUser = UserValidation.FindUser(_userRepository, request.Id).Result;
        
        if (findUser.IsFailed)
        {
            return findUser.ToResult().ToUserResultDto(null);
        }
        
        var user = findUser.Value;
        _userRepository.Remove(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Result.Ok().ToUserResultDto(user);
    }
}