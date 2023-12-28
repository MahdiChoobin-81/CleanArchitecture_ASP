using Movie_asp.Dto;
using Movie_asp.Entities;

namespace Application.Dto.Entities;

// public class UpdateMovieDto
// {
//     public string MovieName { get; set; } 
//     public string MovieDescription { get; set; } 
//     public sbyte MovieRate { get; set; } 
//     public DateTime ReleaseDate { get; set; } 
//     public string MainImage { get; set; } 
//     public bool Subtitle { get; set; } 
//     public List<MovieImageDto> MovieImages { get; set; }
//     public List<GenreDto>  MovieGenres { get; set; }
// }

public record UpdateMovieDto(
    string MovieName,
    string MovieDescription,
    sbyte MovieRate,
    DateTime ReleaseDate,
    string MainImage,
    bool Subtitle,
    List<MovieImageDto> MovieImages,
    List<Guid>  GenreIds
    );
