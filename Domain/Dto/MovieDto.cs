using System.Collections;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Movie_asp.Dto;

public class MovieDto
{
    public string MoiveName { get; set; }
    public string MovieDescription { get; set; }
    public sbyte MovieRate { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string MainImage { get; set; }
    public bool Subtitle { get; set; }
    public List<string> Images { get; set; }
    public List<Guid> GenreIds { get; set; }
    
    
}