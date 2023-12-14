using FluentResults;
using Movie_asp.Entities;

namespace Application.Dto.Results;

public static class GenreResultDtoExtensions
{
    public static GenreResultDto ToGenreResultDto(this Result result, Genre? genre)
    {
        if (result.IsSuccess)
            return new GenreResultDto(false, null, genre);

        return new GenreResultDto(true, TransformErrors(result.Errors), null);
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