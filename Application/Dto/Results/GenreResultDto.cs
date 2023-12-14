using Movie_asp.Entities;

namespace Application.Dto.Results;

public class GenreResultDto
{
    public bool IsFailed { get; set; }

    public IEnumerable<ErrorDto>? Errors { get; set; }
    public Genre? Genre { get; set; }
    

    public GenreResultDto(bool isFailed, IEnumerable<ErrorDto>? errors, Genre? genre)
    {
        IsFailed = isFailed;
        Errors = errors;
        Genre = genre;
    }
}