using FluentResults;
using Movie_asp.Entities;

namespace Application.Dto.Results;

public static class LanguageResultDtoExtensions
{
    public static LanguageResultDto ToLanguageResultDto(this Result result, Language? language)
    {
        if (result.IsSuccess)
            return new LanguageResultDto(false, null, language);

        return new LanguageResultDto(true, TransformErrors(result.Errors), null);
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