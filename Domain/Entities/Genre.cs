using System.ComponentModel.DataAnnotations;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Genre;

namespace Movie_asp.Entities;

public class Genre
{
    public Genre(Id id, GenreName genreName)
    {
        Id = id;
        GenreName = genreName;
    }

    public Id Id { get; private set; }
    [MaxLength(50)]
    public GenreName GenreName { get; private set; }

    public IList<MovieCollection> MovieCollection { get; private set; } 


    public void Update(GenreName genreName)
    {
        GenreName = genreName;
    }
}