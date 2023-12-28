using System.Collections;
using System.ComponentModel.DataAnnotations;
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
    // public IList<MovieCollection> MovieCollection { get; private set; } 



    public void Update(CountryName countryName)
    {
        CountryName = countryName;
    }



}