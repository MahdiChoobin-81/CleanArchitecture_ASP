using FluentResults;

namespace Movie_asp.ValueObjects.Language;

public record LanguageName
{
    private const byte MaxLength = 50;

    private LanguageName(string value)
    {
        Value = value;
    }
    
    public string Value { get; }


    public static Result<LanguageName?> Create(string value)
    {

        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail("Language Name cannot be Empty.");
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail("Language Name cannot be more than " + MaxLength + " characters.");
        }

        return new LanguageName(value);

    }
        
}