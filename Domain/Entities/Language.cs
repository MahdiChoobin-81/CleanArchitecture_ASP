using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Language;

namespace Movie_asp.Entities;

public class Language : IDomain
{
    public Language(Id id, LanguageName languageName)
    {
        Id = id;
        LanguageName = languageName;
    }

    public Id Id { get; private set; }
    [MaxLength(50)]
    public LanguageName LanguageName { get; private set; }
    
    [JsonIgnore]
    public List<Movie> Movies { get; set; }

    public void Update(LanguageName languageName)
    {
        LanguageName = languageName;
    }
    
    
}