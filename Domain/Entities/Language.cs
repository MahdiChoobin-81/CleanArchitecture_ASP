using System.ComponentModel.DataAnnotations;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Language;

namespace Movie_asp.Entities;

public class Language
{
    public Language(Id id, LanguageName languageName)
    {
        Id = id;
        LanguageName = languageName;
    }

    public Id Id { get; private set; }
    [MaxLength(50)]
    public LanguageName LanguageName { get; private set; }
    
    public IList<MovieCollection> MovieCollections { get; private set; }

    public void Update(LanguageName languageName)
    {
        LanguageName = languageName;
    }
    
    
}