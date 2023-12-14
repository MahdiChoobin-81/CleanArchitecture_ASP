using Movie_asp.ValueObjects;

namespace Movie_asp.Entities;

public class MovieCollection
{
    public MovieCollection(Id movieId,
        Id countryId,
        Id genreId,
        Id languageId,
        Id actorId
        )
    {
        MovieId = movieId;
        CountryId = countryId;
        GenreId = genreId;
        LanguageId = languageId;
        ActorId = actorId;
    }

    public Id MovieId { get; private set; } 
    public Id GenreId { get; private set; }  
    public Id CountryId { get; private set; } 
    public Id LanguageId { get; private set; } 
    public Id ActorId { get; private set; }
    
    
    public Movie Moive { get; private set; }
    public Country Country { get; private set; }
    public Genre Genre { get; private set; }
    public Language Language { get; private set; } 
    public Actor Actor { get; private set; }
    
    


    public void Update(Id movieId, Id countryId, Id genreId, Id languageId, Id actorId)
    {
        MovieId = movieId;
        CountryId = countryId;
        GenreId = genreId;
        LanguageId = languageId;
        ActorId = actorId;
    }
}