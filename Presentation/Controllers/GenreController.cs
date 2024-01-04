using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Genre;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class GenreController : Controller

{
    
    private readonly IGenreRepository _genreRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GenreController(IUnitOfWork unitOfWork, IGenreRepository genreRepository)
    {
        _unitOfWork = unitOfWork;
        _genreRepository = genreRepository;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<CustomGenericResult>> GetById(Guid id)
    {
        var result = await FindEntityRecordById<Genre>.Find(
            _genreRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var genre = result.Value;
        return Result.Ok().ToCustomGenericResult(genre, Movie_asp.StatusCode.Ok);
        
    }

    [HttpGet]
    public async Task<IEnumerable<Genre>> GetAllAsync()
    {
        return await _genreRepository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<CustomGenericResult>> Post([FromBody] string genreName)
    {
        var name = GenreName.Create(genreName);

        if (name.IsFailed)
        {
            return BadRequest(name.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        }


        var genre = new Genre(
            new Id(Guid.NewGuid()),
            name.Value);
        

        var result = await _genreRepository.Add(genre);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = genre.Id.Value }, genre);

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Delete(Guid id)
    {
        var result = await FindEntityRecordById<Genre>.Find(
            _genreRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var genre = result.Value;
        
        _genreRepository.Remove(genre);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(genre, Movie_asp.StatusCode.Ok);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Put(Guid id, [FromBody] string genreName)
    
    {
        var findGenre = await FindEntityRecordById<Genre>.Find(
            _genreRepository, new Id(id));

        if (findGenre.IsFailed)
        {
            return findGenre.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var genre = findGenre.Value;
        var currentGenreName = genre.GenreName;
        var name = GenreName.Create(genreName);

        if (name.IsFailed)
            return BadRequest(name.ToResult()
                .ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        
        genre.Update(name.Value);

        var result = await _genreRepository.Update(genre, currentGenreName);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(genre, Movie_asp.StatusCode.Ok);
        
    }






}