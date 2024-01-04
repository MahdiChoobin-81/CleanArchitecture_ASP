

using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]



public class CountryController : Controller

{
    
    private readonly ICountryRepository _countryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CountryController(IUnitOfWork unitOfWork, ICountryRepository countryRepository)
    {
        _unitOfWork = unitOfWork;
        _countryRepository = countryRepository;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CustomGenericResult>> GetById(Guid id)
    {
        var result = await FindEntityRecordById<Country>.Find(
            _countryRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var country = result.Value;
        return Result.Ok().ToCustomGenericResult(country, Movie_asp.StatusCode.Ok);
        
    }

    [HttpGet]
    public async Task<IEnumerable<Country>> GetAllAsync()
    {
        return await _countryRepository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<CustomGenericResult>> Post([FromBody] string countryName)
    {
        var name = CountryName.Create(countryName);

        if (name.IsFailed)
        {
            return BadRequest(name.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        }


        var country = new Country(
            new Id(Guid.NewGuid()),
            name.Value);

        var result = await _countryRepository.Add(country);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = country.Id.Value }, country);

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Delete(Guid id)
    {
        var result = await FindEntityRecordById<Country>.Find(
            _countryRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var country = result.Value;
        
        _countryRepository.Remove(country);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(country, Movie_asp.StatusCode.Ok);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Put(Guid id, [FromBody] string countryName)
    
    {
        var findcountry = await FindEntityRecordById<Country>.Find(
            _countryRepository, new Id(id));

        if (findcountry.IsFailed)
        {
            return findcountry.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var country = findcountry.Value;
        var currentCountryName = country.CountryName;
        var name = CountryName.Create(countryName);

        if (name.IsFailed)
            return BadRequest(name.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        
        country.Update(name.Value);

        var result = await _countryRepository.Update(country, currentCountryName);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(country, Movie_asp.StatusCode.Ok);
        
    }

}