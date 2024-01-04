using FluentResults;

namespace Movie_asp.ValueObjects.User;
// [ComplexType]
public record Password
{
    private const byte MinLength = 8;
    private const byte MaxLength = 30;
    public string Value { get; }
    private Password(string value)
    {
        Value = value;
    }

    public static Result<Password?> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            return Result.Fail("Password cannot be empty.");
        if (value.Length < MinLength)
            return Result.Fail("Password cannot be less than  " + MinLength + " character.");
        if (value.Length > MaxLength)
            return Result.Fail("Password cannot be more than  " + MaxLength + " character.");
        return new Password(value);
    }
}