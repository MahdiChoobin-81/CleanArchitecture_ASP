
using System.ComponentModel.DataAnnotations.Schema;
using FluentResults;

namespace Movie_asp.ValueObjects;
// [ComplexType]
public record UserFullName
{
    private const byte MaxLenght = 50;
    

    private UserFullName(string value)
    {
        Value = value;
    }
    
    public string Value { get; }


    public static Result<UserFullName> Create(string value)
    {
        
        if (string.IsNullOrWhiteSpace(value))
        {
           return Result.Fail("Full Name cannot be empty.");
        }

        if (value.Length > MaxLenght)
        {
            return Result.Fail("Full Name cannot be more than 50 character.");

        }

        return new UserFullName(value);
    }

    // public static implicit operator string(UserFullName fullName)
    //     => fullName.Value;
    //
    // public static implicit operator UserFullName(string fullName)
    //     => new(fullName);


}
