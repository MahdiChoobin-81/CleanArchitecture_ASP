namespace Application.Dto.Results;

public class ErrorDto
{
    public string Message { get; set; }


    public ErrorDto(string message)
    {
        Message = message;
    }
}