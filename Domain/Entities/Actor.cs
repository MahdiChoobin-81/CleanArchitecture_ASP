using System.ComponentModel.DataAnnotations;
using Movie_asp.ValueObjects;

namespace Movie_asp.Entities;

public class Actor
{
    public Actor(Id id, FullName fullName, Image img)
    {
        Id = id;
        FullName = fullName;
        Img = img;
    }

    public Id Id { get; private set; }
    [MaxLength(50)]
    public FullName FullName { get; private set; }
    [MaxLength(120)]
    public Image Img { get; private set; }
    public IList<MovieCollection> MovieCollections { get; private set; }


    public void Update(FullName fullName, Image image)
    {
        FullName = fullName;
        Img = image;
    }
}