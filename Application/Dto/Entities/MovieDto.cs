namespace Application.Dto.Entities;

public record MovieDto(
    string MovieName,
    string MovieDescription,
    sbyte MovieRate,
    DateTime ReleaseDate,
    string MainImage,
    bool Subtitle,
    List<MovieImageDto> MovieImages,
    List<Guid> GenreIds,
    List<Guid> LanguagesIds,
    List<Guid> CountriesIds,
    List<Guid> ActorsIds
);