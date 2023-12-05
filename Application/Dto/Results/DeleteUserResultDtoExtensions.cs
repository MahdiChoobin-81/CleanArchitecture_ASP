//
// using FluentResults;
// using Movie_asp.Entities;
//
// namespace Application.Dto.Results;
//
// public static class DeleteUpdateUserResultDtoExtensions
// {
//     public static DeleteUserResultDto ToDeleteUserResultDto(this Result result, User? user)
//     {
//         if (result.IsFailed)
//             return new DeleteUserResultDto(true, TransformErrors(result.Errors), null);
//
//         return new DeleteUserResultDto(false, null, user);
//     }
//     
//     private static IEnumerable<ErrorDto>? TransformErrors(List<IError> errors)
//     {
//         return errors.Select(TransformError);
//     }
//
//     private static ErrorDto TransformError(IError error)
//     {
//         return new ErrorDto(error.Message);
//     }
// }