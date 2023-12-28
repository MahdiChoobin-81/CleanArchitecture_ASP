using Application.Dto.Entities;
using Movie_asp;
using Movie_asp.Dto;
using Movie_asp.Entities;

namespace Application.Dto.Results;

public class MovieResultDto
{
    public bool IsFailed { get; set; }

    public IEnumerable<ErrorDto>? Errors { get; set; }
    public Movie? Movie { get; set; }
    public StatusCode StatusCode { get; set; }
    

    public MovieResultDto(bool isFailed, IEnumerable<ErrorDto>? errors, Movie? movie, StatusCode statusCode)
    {
        IsFailed = isFailed;
        Errors = errors;
        Movie = movie;
        StatusCode = statusCode;
    }
}