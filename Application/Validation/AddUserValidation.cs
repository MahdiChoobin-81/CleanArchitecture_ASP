using FluentResults;
using Movie_asp.ValueObjects;

namespace Application.Validation;

public class AddUserValidation
{
    
    public static Result IsValid(Result<UserFullName> fullName,
        Result<Username> username, Result<Password> password, Result<Email> email)
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