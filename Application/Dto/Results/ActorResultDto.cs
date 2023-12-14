using Movie_asp.Entities;

namespace Application.Dto.Results;

public class ActorResultDto
{
    public bool IsFailed { get; set; }

    public IEnumerable<ErrorDto>? Errors { get; set; }
    public Actor? Actor { get; set; }
    

    public ActorResultDto(bool isFailed, IEnumerable<ErrorDto>? errors, Actor? actor)
    {
        IsFailed = isFailed;
        Errors = errors;
        Actor = actor;
    }
}