using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Movie_asp.ValueObjects;

namespace Movie_asp.Entities;

public class Actor : IDomain
{
    public Actor(Id id, FullName actorName, Image img)
    {
        Id = id;
        ActorName = actorName;
        Img = img;
    }

    public Id Id { get; private set; }
    [MaxLength(50)]
    public FullName ActorName { get; private set; }
    [MaxLength(120)]
    public Image Img { get; private set; }
    
    [JsonIgnore]
    public List<Movie> Movies { get; set; }


    public void Update(FullName actorName, Image image)
    {
        ActorName = actorName;
        Img = image;
    }
}