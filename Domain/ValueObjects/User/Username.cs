using FluentResults;

namespace Movie_asp.ValueObjects.User;
public record Username
{
    private const byte MaxLength = 30;
    public string Value { get; }

    private Username(string value)
    {

        Value = value;
    }

    public static Result<Username?> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail("Username cannot be empty.");
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail("Username cannot be more than  " + MaxLength + " characters");
        }

        return new Username(value);
    }

    // public static implicit operator string(Username username)
    //     => username.Value;
    //
    // public static implicit operator Username(string value)
    //     => new(value);
}