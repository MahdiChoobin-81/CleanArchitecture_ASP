using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Genre;

namespace Movie_asp.Entities;

public class Genre : IDomain
{
    public Genre(Id id, GenreName genreName)
    {
        Id = id;
        GenreName = genreName;
    }

    public Id Id { get; private set; }
    [MaxLength(50)]
    public GenreName GenreName { get; private set; }
    [JsonIgnore]
    public List<Movie> Movies { get; set; }


    public void Update(GenreName genreName)
    {
        GenreName = genreName;
    }

}