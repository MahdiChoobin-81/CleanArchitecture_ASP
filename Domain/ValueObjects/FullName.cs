
using System.ComponentModel.DataAnnotations.Schema;
using FluentResults;

namespace Movie_asp.ValueObjects;
public record FullName
{
    private const byte MaxLength = 50;
    

    public string Value { get; }
    private FullName(string value)
    {
        Value = value;
    }
    public static Result<FullName?> Create(string value)
    {
        
        if (string.IsNullOrWhiteSpace(value))
        {
           return Result.Fail("Full Name cannot be empty.");
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail("Full Name cannot be more than " + MaxLength + " characters.");

        }

        return new FullName(value);
    }

    // public static implicit operator string(UserFullName fullName)
    //     => fullName.Value;
    //
    // public static implicit operator UserFullName(string fullName)
    //     => new(fullName);


}
