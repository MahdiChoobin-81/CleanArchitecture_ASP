using FluentResults;
using Movie_asp.Entities;

namespace Application.Dto.Results;

public static class ActorResultDtoExtensions
{
    public static ActorResultDto ToActorResultDto(this Result result, Actor? actor)
    {
        if (result.IsSuccess)
            return new ActorResultDto(false, null, actor);

        return new ActorResultDto(true, TransformErrors(result.Errors), null);
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