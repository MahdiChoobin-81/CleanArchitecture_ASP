using Movie_asp.Entities;

namespace Application.Dto.Results;

public class MovieResultDto
{
    public bool IsFailed { get; set; }

    public IEnumerable<ErrorDto>? Errors { get; set; }
    public Movie? Movie { get; set; }
    

    public MovieResultDto(bool isFailed, IEnumerable<ErrorDto>? errors, Movie? movie)
    {
        IsFailed = isFailed;
        Errors = errors;
        Movie = movie;
    }
}