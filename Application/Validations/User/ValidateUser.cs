using FluentResults;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Application.Validations.User;

public static class ValidateUser
{
    public static Result Validate(
        Result<FullName> fullName,
        Result<Username> username,
        Result<Password> password,
        Result<Email> email)
    {
        
        if (fullName.IsFailed)
        {
            return fullName.ToResult();
        }

        if (username.IsFailed)
        {
            return username.ToResult();
        }

        if (password.IsFailed)
        {
            return password.ToResult();
        }

        if (email.IsFailed)
        {
            return email.ToResult();
        }

        return Result.Ok();
    }
    
    
}