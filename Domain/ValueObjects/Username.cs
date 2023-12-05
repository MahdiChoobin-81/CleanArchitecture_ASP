
using FluentResults;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movie_asp.ValueObjects;
// [ComplexType]
public record Username
{

    public const byte MaxLength = 30;
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
            return Result.Fail("Username cannot be more than 30 characters");
        }

        return new Username(value);
    }

    // public static implicit operator string(Username username)
    //     => username.Value;
    //
    // public static implicit operator Username(string value)
    //     => new(value);
}