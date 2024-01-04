using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Movie_asp.ValueObjects;

namespace Movie_asp.Entities;

public class Country : IDomain
{
    public Country(Id id, CountryName countryName)
    {
        Id = id;
        CountryName = countryName;
    }

    public Id Id { get; private set; }
    [MaxLength(60)]
    public CountryName CountryName { get; private set; }
    [JsonIgnore]
    public List<Movie> Movies { get; set; }



    public void Update(CountryName countryName)
    {
        CountryName = countryName;
    }



}