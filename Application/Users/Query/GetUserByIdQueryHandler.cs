using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using MediatR;
using Movie_asp.Entities;
using Movie_asp.Repositories;

namespace Application.Users.Query;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResultDto>
{
    private readonly IUserRepository _userRepository;

    public GetUserByIdQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResultDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
    
        
        var findUser = UserValidation.FindUser(_userRepository, request.UserId).Result;
        
        if (findUser.IsFailed)
        {
            return findUser.ToResult().ToUserResultDto(null);
        }
        var user = findUser.Value;
        
        
        
        return Result.Ok().ToUserResultDto(user);

    }
}