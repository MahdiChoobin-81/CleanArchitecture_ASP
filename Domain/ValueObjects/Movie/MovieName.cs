using FluentResults;

namespace Movie_asp.ValueObjects.Movie;

public record MovieName
{
    private MovieName(string value)
    {
        Value = value;
    }

    private const byte MaxLength = 120;
    
    public string Value { get; }


    public static Result<MovieName?> Create(string movieName)
    {
        if (string.IsNullOrWhiteSpace(movieName))
            return Result.Fail("Name of the Movie cannot be empty.");

        if (movieName.Length > MaxLength)
            return Result.Fail("Name of the Movie cannot be more than " + MaxLength + "  character.");

        return new MovieName(movieName);
    }
    
    
}