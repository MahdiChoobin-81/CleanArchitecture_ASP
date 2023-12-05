// using Application.Dto.Results;
// using FluentResults;
// using Movie_asp.Entities;
// using Movie_asp.ValueObjects;
//
// namespace Application.Validations;
//
// public static class AddUserValidation
// {
//   
//     public static AddUserResultDto IsValid(UserId id, Result<UserFullName> fullName,
//         Result<Username> username, Result<Password> password, Result<Email> email, CreatedAt createdAt)
//     {
//         if (fullName.IsFailed)
//         {
//             return fullName.ToResult().ToAddUserResultDto(null);
//         }
//         if (username.IsFailed)
//         {
//             return username.ToResult().ToAddUserResultDto(null);
//         }
//         if (password.IsFailed)
//         {
//             return password.ToResult().ToAddUserResultDto(null);
//         }
//         if (email.IsFailed)
//         {
//             return email.ToResult().ToAddUserResultDto(null);
//         }
//         var user = new User(
//             id,
//             fullName.Value,
//             username.Value,
//             password.Value,
//             email.Value,
//             createdAt
//         );
//
//         return Result.Ok().ToAddUserResultDto(user);
//     }
//     
// }