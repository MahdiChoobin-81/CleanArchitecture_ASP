using Application.Data;
using Application.Dto.Results;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Genre;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class GenreController : Controller
{

    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IUnitOfWork _unitOfWork;

    public GenreController(IApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
    {
        _applicationDbContext = applicationDbContext;
        _unitOfWork = unitOfWork;
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<GenreResultDto>> GetById(Guid id)
    {
        var result = await _applicationDbContext.Genres
            .FirstOrDefaultAsync(g => g.Id == new Id(id));

        if (result is null)
        {
            return NotFound(Result.Fail("There's no Genre with This Id : " + id).ToGenreResultDto(null));
        }

        return Ok(Result.Ok().ToGenreResultDto(result));
        
    }

    [HttpGet]
    public async Task<ActionResult<GenreResultDto>> GetAllAsync()
    {
        var result = await _applicationDbContext.Genres.ToListAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<GenreResultDto>> Post([FromBody] string genreName)
    {
        var name = GenreName.Create(genreName);

        if (name.IsFailed)
        {
            return BadRequest(name.ToResult().ToGenreResultDto(null));
        }


        var genre = new Genre(
            new Id(Guid.NewGuid()),
            name.Value!
        );

        _applicationDbContext.Genres.Add(genre);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = genre.Id }, genre);

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<GenreResultDto>> Delete(Guid id)
    {
        var genre = await _applicationDbContext.Genres
            .FirstOrDefaultAsync(g => g.Id == new Id(id));

        if (genre is null)
            return NotFound(Result.Fail("There's no Genre with This Id : " + id).ToGenreResultDto(null));

        _applicationDbContext.Genres.Remove(genre);
        await _unitOfWork.SaveChangesAsync();
        return Ok(genre);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<GenreResultDto>> Put(Guid id, [FromBody] string genreName)
    {
        var genre = await _applicationDbContext.Genres
            .FirstOrDefaultAsync(g => g.Id == new Id(id));

        if (genre is null)
        {
            return NotFound(Result.Fail("There's no Genre with This Id : " + id).ToGenreResultDto(null));
        }

        var name = GenreName.Create(genreName);

        if (name.IsFailed)
            return BadRequest(name.ToResult().ToGenreResultDto(null));
        
        genre.Update(name.Value!);

        _applicationDbContext.Genres.Update(genre);
        await _unitOfWork.SaveChangesAsync();
        return Ok(genre);
        
    }






}