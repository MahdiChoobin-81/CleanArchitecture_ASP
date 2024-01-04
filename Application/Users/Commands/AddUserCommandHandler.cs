
using Application.Data;
using Application.Dto.Results;
using Application.Users.Query;
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

internal sealed class AddUserCommandHandler : IRequestHandler<AddUserCommand, CustomGenericResult>
{

    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    
    public AddUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    
    
    public async Task<CustomGenericResult> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        

        var createUserResult = CreateUserInstance.Create(
            request.FullName,
            request.Username,
            request.Password,
            request.Email);

        if (createUserResult.IsFailed)
            return createUserResult.ToResult().ToCustomGenericResult(null, StatusCode.BadRequest);
        
        var user = createUserResult.Value;
     
        var result = await _userRepository.Add(user);

        if (result.IsFailed)
            return result.ToCustomGenericResult(null, StatusCode.BadRequest);
        
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Ok().ToCustomGenericResult(user, StatusCode.Created);
  
    }
}