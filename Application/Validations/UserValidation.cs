using Application.Dto.Results;
using FluentResults;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Application.Validations;

public static class UserValidation
{

    

    public static async Task<Result<User>> FindUser(IUserRepository userRepository,Id id)
    {
        var user = await userRepository.GetByIdAsync(id);

        if (user is null)
        {
            return Result.Fail("There's no User with this Id");
        }

        return Result.Ok(user);
    }
    
    public static UserResultDto IsValid(Id id, Result<FullName> fullName,
        Result<Username> username, Result<Password> password, Result<Email> email, CreatedAt createdAt)
    {
        if (fullName.IsFailed)
        {
            return fullName.ToResult().ToUserResultDto(null);
        }
        if (username.IsFailed)
        {
            return username.ToResult().ToUserResultDto(null);
        }
        if (password.IsFailed)
        {
            return password.ToResult().ToUserResultDto(null);
        }
        if (email.IsFailed)
        {
            return email.ToResult().ToUserResultDto(null);
        }
        var user = new User(
            id,
            fullName.Value,
            username.Value,
            password.Value,
            email.Value,
            createdAt
        );

        return Result.Ok().ToUserResultDto(user);
    }
}