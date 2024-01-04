using System.ComponentModel.DataAnnotations;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Movie;

namespace Movie_asp.Entities;

public class Movie
{
    public Movie(
        Id id,
        MovieName movieName,
        MovieDescription movieDescription,
        MovieRate movieRate,
        ReleaseDate releaseDate,
        Image mainImage,
        bool subtitle,
        CreatedAt createdAt
    )
    {
        Id = id;
        MovieName = movieName;
        MovieDescription = movieDescription;
        MovieRate = movieRate;
        ReleaseDate = releaseDate;
        MainImage = mainImage;
        Subtitle = subtitle;
        CreatedAt = createdAt;
    }

    public Id Id { get; private set; }
    [MaxLength(120)]
    public MovieName MovieName { get; private set; }
    [MaxLength(800)]
    public MovieDescription MovieDescription { get; private set; }
    [MaxLength(5)]
    public MovieRate MovieRate { get; private set; }
    public ReleaseDate ReleaseDate { get; private set; }
    [MaxLength(120)]
    public Image MainImage { get; private set; }
    
    public bool Subtitle { get; private set; }
    public CreatedAt CreatedAt { get; private set; }
    public List<MovieImage> MoiveImages { get; set; }
    public List<Genre> Genres { get; set; }
    public List<Language> Languages { get; set; }
    public List<Country> Countries { get; set; }
    public List<Actor> Actors { get; set; }

    public void Update(
        MovieName movieName,
        MovieDescription movieDescription,
        MovieRate movieRate,
        ReleaseDate releaseDate,
        Image mainImage,
        bool subtitle
        )
    {
        MovieName = movieName;
        MovieDescription = movieDescription;
        MovieRate = movieRate;
        ReleaseDate = releaseDate;
        MainImage = mainImage;
        Subtitle = subtitle;
    }
    
    
}