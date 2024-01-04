using Movie_asp;
using Movie_asp.Entities;

namespace Application.Dto.Results;

public class CustomGenericResult
{
    public bool IsFailed { get; set; }

    public IEnumerable<ErrorDto>? Errors { get; set; }
    public object? Data { get; set; }
    public StatusCode StatusCode { get; set; }
    

    public CustomGenericResult(bool isFailed, IEnumerable<ErrorDto>? errors, object? data, StatusCode statusCode)
    {
        IsFailed = isFailed;
        Errors = errors;
        Data = data;
        StatusCode = statusCode;
    }
}