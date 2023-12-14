using System.ComponentModel.DataAnnotations;
using Movie_asp.ValueObjects;

namespace Movie_asp.Entities;

public class MoiveImage
{
    public Id Id { get; private set; }
    [MaxLength(120)]
    public Image Image { get; private set; }
    
    public Id MovieId { get; private set; }
    public Movie Movie { get; private set; }


    public MoiveImage(Image image)
    {
        Image = image;
    }
    
}