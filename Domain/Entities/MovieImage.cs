using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Movie_asp.ValueObjects;

namespace Movie_asp.Entities;

public class MovieImage
{
    public Id Id { get; private set; }
    [MaxLength(120)]
    public Image Image { get; private set; }
    
    public Id MovieId { get; set; }
    [JsonIgnore]
    public Movie Movie { get; private set; }
    
    public MovieImage(Id id, Image image)
    {
        Id = id;
        Image = image;
    }
    
}


