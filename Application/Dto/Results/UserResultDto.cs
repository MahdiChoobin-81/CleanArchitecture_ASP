using Movie_asp.Entities;

namespace Application.Dto.Results;

public class UserResultDto
{
    public bool IsFailed { get; set; }

    public IEnumerable<ErrorDto>? Errors { get; set; }
    public User? User { get; set; }
    

    public UserResultDto(bool isFailed, IEnumerable<ErrorDto>? errors, User? user)
    {
        IsFailed = isFailed;
        Errors = errors;
        User = user;
    }
}