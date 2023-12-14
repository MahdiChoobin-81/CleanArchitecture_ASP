using FluentResults;

namespace Movie_asp.ValueObjects;

public record CountryName
{
    private const byte MaxLength = 60;
    

    public string Value { get; }
    private CountryName(string value)
    {
        Value = value;
    }
    public static Result<CountryName?> Create(string value)
    {
        
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail("Country cannot be empty.");
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail("Country cannot be more than " + MaxLength + " characters.");

        }

        return new CountryName(value);
    }
}