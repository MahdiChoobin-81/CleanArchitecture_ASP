using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Application.Users.Commands;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResultDto>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserResultDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        
        var findUser = UserValidation.FindUser(_userRepository, request.UserId).Result;
        
        if (findUser.IsFailed)
        {
            return findUser.ToResult().ToUserResultDto(null);
        }
        var user = findUser.Value;
        
        

        var id = user.Id;
        var fullNameResult = UserFullName.Create(request.FullName);
        var usernameResult = Username.Create(request.Username);
        var passwordResult = Password.Create(request.Password);
        var emailResult = Email.Create(request.Email);
        var createdAt = user.CreatedAt;
        
        
        var validation = UserValidation.IsValid(id, fullNameResult, usernameResult, passwordResult, emailResult, createdAt);

        if (validation.IsFailed)
        {
            return validation;
        }

        user.Update(validation.User?.FullName!,
            validation.User?.Username!,
            validation.User?.Password!,
            validation.User?.Email!);

        
        
        
        _userRepository.Update(user);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return validation;
    }
}