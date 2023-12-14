using FluentResults;
using Movie_asp.Entities;

namespace Application.Dto.Results;

public static class MovieResultDtoExtensions
{
    public static MovieResultDto ToMovieResultDto(this Result result, Movie? movie)
    {
        if (result.IsSuccess)
            return new MovieResultDto(false, null, movie);

        return new MovieResultDto(true, TransformErrors(result.Errors), null);
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