using System.ComponentModel.DataAnnotations.Schema;
using FluentResults;

namespace Movie_asp.ValueObjects;
// [ComplexType]
public record Password
{
    public const byte MinLength = 8;
    public const byte MaxLength = 30;
    public string Value { get; }
    private Password(string value)
    {
        Value = value;
    }

    public static Result<Password> Create(string value)
    {
        if(string.IsNullOrWhiteSpace(value))
            return Result.Fail("Password cannot be empty.");
        if (value.Length < MinLength)
            return Result.Fail("Password cannot be less than 8 character.");
        if (value.Length > MaxLength)
            return Result.Fail("Password cannot be more than 30 character.");
        return new Password(value);
    }
}