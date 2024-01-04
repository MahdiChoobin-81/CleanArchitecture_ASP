using FluentResults;
using Movie_asp;

namespace Application.Dto.Results;

public static class CustomGenericResultExtension
{
    public static CustomGenericResult ToCustomGenericResult(this Result result, object? data, StatusCode statusCode)
    {
        if (result.IsSuccess)
            return new CustomGenericResult(false, null, data, statusCode);

        return new CustomGenericResult(true, TransformErrors(result.Errors), null, statusCode);
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