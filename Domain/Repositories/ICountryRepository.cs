using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Movie_asp.Repositories;

public interface ICountryRepository : IRepository<Country>
{
    Task<Result> Add(Country country);

    void Remove(Country country);

    Task<Result> Update(Country country, CountryName currentCountryName);

    Task<IEnumerable<Country>> GetAllAsync();
}