using Application;
using Application.Dto.Entities;
using Application.Dto.Results;
using Application.Movies.Commands;
using Application.Movies.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class MovieController : Controller
{

    private readonly ISender _sender;

    public MovieController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<CustomGenericResult>> Post([FromBody] MovieDto input)
    {
        var command = new AddMovieCommand(
            input.MovieName,
            input.MovieDescription,
            input.MovieRate,
            input.ReleaseDate,
            input.MainImage,
            input.Subtitle,
            input.MovieImages,
            input.GenreIds,
            input.LanguagesIds,
            input.CountriesIds,
            input.ActorsIds
            );

        var result = await _sender.Send(command);

            switch (result.StatusCode)
            {
                case Movie_asp.StatusCode.BadRequest:
                    return BadRequest(result);
                case Movie_asp.StatusCode.NotFound:
                    return NotFound(result);
                default:
                    var movieId = ObjectConverter<Movie>.Convert(result.Data).Id.Value;
                    return CreatedAtAction(nameof(GetById), new{ id = movieId}, result);
                
            }
            
    }


    [HttpGet]
    public async Task<IEnumerable<Movie>> GetAll()
    {
        return await _sender.Send(new GetAllMoviesQuery());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomGenericResult>> GetById(Guid id)
    {
        var result = await _sender.Send(new GetMovieByIdQuery(new Id(id)));
        
        if (result.IsFailed)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Delete(Guid id)
    {
        var result = await _sender.Send(new DeleteMovieCommand(new Id(id)));

        if (result.IsFailed)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Put(Guid id, [FromBody] MovieDto movieDto)
    {
        var result = await _sender.Send(new UpdateMovieCommand(new Id(id), movieDto));
    
        if (result.StatusCode == Movie_asp.StatusCode.NotFound)
            return NotFound(result);
        
        if (result.StatusCode == Movie_asp.StatusCode.BadRequest)
            return BadRequest(result);
    
        return Ok(result);
    
    }
    
    
    
    
}