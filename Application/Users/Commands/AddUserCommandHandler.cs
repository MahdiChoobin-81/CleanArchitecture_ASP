using System.ComponentModel.DataAnnotations;
using Application.Data;
using Application.Validation;
using Domain.Entities;
using Domain.Repositories;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Application.Users.Commands;

public class AddUserCommandHandler : IRequestHandler<AddUserCommand, Result<User?>>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    
    public AddUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<Result<User?>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var fullNameResult = UserFullName.Create(request.FullName);
        var usernameResult = Username.Create(request.Username);
        var passwordResult = Password.Create(request.Password);
        var emailResult = Email.Create(request.Email);

        var validation = AddUserValidation.IsValid(fullNameResult, usernameResult, passwordResult, emailResult);

        if (validation.IsFailed)
        {
            return validation;
        }

        var user = new User(
            new UserId(Guid.NewGuid()),
            fullNameResult.Value,
            usernameResult.Value,
            passwordResult.Value,
            emailResult.Value,
            new CreatedAt(DateTime.Now)
            );
        
        _userRepository.Add(user);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        
        
        return user;
    }
}