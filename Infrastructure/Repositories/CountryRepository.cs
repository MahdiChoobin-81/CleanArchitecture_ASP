using Application.Data;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly IApplicationDbContext _context;

    public CountryRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Country?> FindByIdAsync(Id id)
    {
        return await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
    }


    public async Task<Result> Add(Country country)
    {
        IQueryable<Country> countries = _context.Countries.AsQueryable();

        var isCountryNameUnique = await countries
            .Where(c => c.CountryName == country.CountryName).FirstOrDefaultAsync();
        
        if (isCountryNameUnique is not null)
        {
            return Result.Fail("Country already exist.");
        }
        _context.Countries.Add(country);
        return Result.Ok();
    }

    public void Remove(Country Country)
    {
        _context.Countries.Remove(Country);
    }

    public async Task<Result> Update(Country country, CountryName currentCountryName)
    {
        IQueryable<Country> countries = _context.Countries.AsQueryable();

        var isCountryNameUnique = await countries
            .Where(c =>
                c.CountryName != currentCountryName &&
                c.CountryName == country.CountryName)
            .FirstOrDefaultAsync();
            
        if (isCountryNameUnique is not null)
        {
            return Result.Fail("Country already exist.");
        }
        
        _context.Countries.Update(country);
        return Result.Ok();
    }

    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _context.Countries.ToListAsync();
    }
}