
using Application.Data;
using Application.Dto.Results;
using Application.Users.Query;
using Application.Validations;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Application.Users.Commands;

internal sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserResultDto>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<UserResultDto> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var id = new Id(Guid.NewGuid());
        var fullNameResult = FullName.Create(request.FullName);
        var usernameResult = Username.Create(request.Username);
        var passwordResult = Password.Create(request.Password);
        var emailResult    = Email.Create(request.Email);
        var createdAt = new CreatedAt(DateTime.Now);



        var validation = UserValidation.IsValid(id, fullNameResult, usernameResult, passwordResult, emailResult, createdAt);
        
        if (validation.IsFailed)
        {
            return validation;
        }

        var user = validation.User;
         _userRepository.Add(user);
        
         await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        
         return validation;
  
    }
}