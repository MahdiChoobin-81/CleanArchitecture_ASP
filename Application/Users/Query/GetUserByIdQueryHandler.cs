using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using Application.Validations.User;
using FluentResults;
using MediatR;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;

namespace Application.Users.Query;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, CustomGenericResult>
{
    private readonly IUserRepository _userRepository;
    
    public GetUserByIdQueryHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
    }

    public async Task<CustomGenericResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await FindEntityRecordById<User>.Find(
            _userRepository, request.Id);

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, StatusCode.NotFound);
        }
        var user = result.Value;
        
        return Result.Ok().ToCustomGenericResult(user, StatusCode.Ok);

    }
}