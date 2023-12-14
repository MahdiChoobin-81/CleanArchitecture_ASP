using FluentResults;
using Movie_asp.ValueObjects.User;

namespace Movie_asp.ValueObjects.Genre;

public record GenreName
{
    private const byte MaxLength = 50;
    public string Value { get; }

    private GenreName(string value)
    {

        Value = value;
    }

    public static Result<GenreName?> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail("Genre Name cannot be empty.");
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail("Genre Name cannot be more than " + MaxLength + " characters");
        }

        return new GenreName(value);
    }
}