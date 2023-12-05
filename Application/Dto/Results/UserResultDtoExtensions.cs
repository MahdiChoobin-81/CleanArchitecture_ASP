using FluentResults;
using Movie_asp.Entities;

namespace Application.Dto.Results;

public static class UserResultDtoExtensions
{
    public static UserResultDto ToUserResultDto(this Result result, User? user)
    {
        if (result.IsSuccess)
            return new UserResultDto(false, null, user);

        return new UserResultDto(true, TransformErrors(result.Errors), null);
    }

    private static IEnumerable<ErrorDto>? TransformErrors(List<IError> errors)
    {
        return errors.Select(TransformError);
    }

    private static ErrorDto TransformError(IError error)
    {
        return new ErrorDto(error.Message);
    }
}