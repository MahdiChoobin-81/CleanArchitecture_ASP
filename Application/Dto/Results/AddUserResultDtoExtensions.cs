// using FluentResults;
// using Movie_asp.Entities;
//
// namespace Application.Dto.Results;
//
// public static class AddUserResultDtoExtensions
// {
//     public static AddUserResultDto ToAddUserResultDto(this FluentResults.Result result, User? user)
//     {
//         if (result.IsSuccess)
//             return new AddUserResultDto(false, null, user);
//
//         return new AddUserResultDto(true, TransformErrors(result.Errors), null);
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
//
// }