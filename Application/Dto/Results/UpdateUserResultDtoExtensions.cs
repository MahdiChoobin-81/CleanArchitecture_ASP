//
// using FluentResults;
// using Movie_asp.Entities;
//
// namespace Application.Dto.Results;
//
// public static class UpdateUserResultDtoExtensions
// {
//     public static UpdateUserResultDto ToUpdateUserResultDto(this Result result, User? user)
//     {
//         if (result.IsFailed)
//             return new UpdateUserResultDto(true, TransformErrors(result.Errors), null);
//
//         return new UpdateUserResultDto(false, null, user);
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