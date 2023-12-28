using System.Globalization;
using FluentResults;

namespace Movie_asp.ValueObjects;

public record Image
{
    private Image(string value)
    {
        Value = value;
    }

    private const byte MaxLength = 120;
    
    public string Value { get; }
    
    public static Result<Image?> Create(string value)
    {
        
        if (string.IsNullOrWhiteSpace(value))
        {
            return Result.Fail("Image cannot be empty.");
        }

        if (value.Length > MaxLength)
        {
            return Result.Fail("Image length cannot be more than " + MaxLength + " characters.");

        }
        
        if (value.EndsWith(".jpeg") || 
            value.EndsWith(".jpg")  || 
            value.EndsWith(".png"))
        {
            return new Image(value);
        }

        return Result.Fail("please set the image name with jpg, jpeg or png Extention.");
    }
    
}