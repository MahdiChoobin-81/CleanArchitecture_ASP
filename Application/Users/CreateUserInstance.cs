using Application.Validations.User;
using FluentResults;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Application.Users;

public static class CreateUserInstance
{
    public static Result<Movie_asp.Entities.User?> Create(
        string rowFullName,
        string rowUsername,
        string rowPassword,
        string rowEmail)
    {

        var fullName = FullName.Create(rowFullName);
        var username = Username.Create(rowUsername);
        var password = Password.Create(rowPassword);
        var email       = Email.Create(rowEmail);

        var validation = ValidateUser.Validate(
            fullName,
            username,
            password,
            email);

        if (validation.IsFailed)
            return validation;


        var id = new Id(Guid.NewGuid());
        var createdAt = new CreatedAt(DateTime.Now);
        
        var user = new Movie_asp.Entities.User(
            id,
            fullName.Value,
            username.Value,
            password.Value,
            email.Value,
            createdAt
        );

        return Result.Ok(user);
    }


   
}