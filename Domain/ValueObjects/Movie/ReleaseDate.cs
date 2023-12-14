using System.Globalization;
using FluentResults;

namespace Movie_asp.ValueObjects.Movie;

public record ReleaseDate
{
    private ReleaseDate(DateTime value)
    {
        Value = value;
    }

    public DateTime Value { get; }

    public static Result<ReleaseDate?> Create(DateTime releaseDate)
    {

        if (string.IsNullOrWhiteSpace(releaseDate.ToString()))
        {
            return Result.Fail("Release Date cannot be empty.");
        }
        
        if (releaseDate.Date > DateTime.Now.Date)
        {
            return Result.Fail("Release Date cannot be more than current time.");
        }

        return new ReleaseDate(releaseDate);
    }


}