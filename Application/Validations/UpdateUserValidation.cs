// using Application.Dto.Results;
// using FluentResults;
// using Movie_asp.Entities;
// using Movie_asp.ValueObjects;
//
// namespace Application.Validations;
//
// public class UpdateUserValidation
// {
//     public static UpdateUserResultDto IsValid(UserId id,Result<UserFullName> fullName,
//         Result<Username> username, Result<Password> password, Result<Email> email, CreatedAt createdAt)
//     {
//         if (fullName.IsFailed)
//         {
//             return fullName.ToResult().ToUpdateUserResultDto(null);
//         }
//         if (username.IsFailed)
//         {
//             return username.ToResult().ToUpdateUserResultDto(null);
//         }
//         if (password.IsFailed)
//         {
//             return password.ToResult().ToUpdateUserResultDto(null);
//         }
//         if (email.IsFailed)
//         {
//             return email.ToResult().ToUpdateUserResultDto(null);
//         }
//
//         var user = new User(
//             id,
//             fullName.Value,
//             username.Value,
//             password.Value,
//             email.Value,
//             createdAt);
//         
//         
//
//         return Result.Ok().ToUpdateUserResultDto(user);
//     }
//
// }