using FluentResults;

namespace Movie_asp.ValueObjects.Movie;

public record  MovieRate
{
    private MovieRate(sbyte value)
    {
        Value = value;
    }

    public sbyte Value { get; }
    private const byte MaxRate = 5;

    public static Result<MovieRate?> Create(sbyte rate)
    {
        if (string.IsNullOrWhiteSpace(rate.ToString()))
            return Result.Fail("Rate cannot be empty.");
        
        if (rate < 0)
            return Result.Fail("Rate cannot be negative.");

        if (rate > MaxRate)
            return Result.Fail("rate cannot be more than " + MaxRate+".");

        return new MovieRate(rate);
    }
}