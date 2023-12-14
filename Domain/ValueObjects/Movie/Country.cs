using FluentResults;

namespace Movie_asp.ValueObjects.Movie;

public record Country
{
    private Country(string value)
    {
        Value = value;
    }

    
    public string Value { get; }


    public static Result<Country?> Create(string country)
    {
        if (string.IsNullOrWhiteSpace(country))
            return Result.Fail("Country cannot be empty.");

        return new Country(country);
    }
}