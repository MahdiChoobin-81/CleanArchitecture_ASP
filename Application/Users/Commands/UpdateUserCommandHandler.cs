using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using Application.Validations.User;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Application.Users.Commands;

internal sealed class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CustomGenericResult>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CustomGenericResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    { 
        var findUser = await FindEntityRecordById<User>.Find(
            _userRepository, request.Id);

        if (findUser.IsFailed)
        {
            return findUser.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        }

        var user = findUser.Value;
        var currentUserEmail = user.Email;
        var currentUsername = user.Username;

        var fullNameResult = FullName.Create(request.FullName);
        var usernameResult = Username.Create(request.Username);
        var passwordResult = Password.Create(request.Password);
        var emailResult = Email.Create(request.Email);

        var validation = ValidateUser.Validate(
            fullNameResult,
            usernameResult,
            passwordResult,
            emailResult
            );

        if (validation.IsFailed)
            return validation.ToCustomGenericResult(null, StatusCode.BadRequest);


        user.Update(
            fullNameResult.Value,
            usernameResult.Value,
            passwordResult.Value,
            emailResult.Value);
        
        var result = await _userRepository.Update(user, currentUserEmail, currentUsername);
        
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, StatusCode.BadRequest);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);


        return Result.Ok().ToCustomGenericResult(user, StatusCode.Ok);
    }
}