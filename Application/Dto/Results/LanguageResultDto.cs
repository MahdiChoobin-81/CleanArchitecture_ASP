using Movie_asp.Entities;

namespace Application.Dto.Results;

public class LanguageResultDto
{
    public bool IsFailed { get; set; }

    public IEnumerable<ErrorDto>? Errors { get; set; }
    public Language? Language { get; set; }
    

    public LanguageResultDto(bool isFailed, IEnumerable<ErrorDto>? errors, Language? language)
    {
        IsFailed = isFailed;
        Errors = errors;
        Language = language;
    }
}