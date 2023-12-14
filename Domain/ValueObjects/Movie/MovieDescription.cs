using FluentResults;

namespace Movie_asp.ValueObjects.Movie;

public record MovieDescription
{
    private MovieDescription(string value)
    {
        Value = value;
    }

    public string Value { get; }
    private const int MaxLength = 800;

    public static Result<MovieDescription?> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return Result.Fail("Name of the Movie cannot be empty.");

        if (value.Length > MaxLength)
            return Result.Fail("Description cannot be more than " + MaxLength + "  character.");

        return new MovieDescription(value);
    }
}