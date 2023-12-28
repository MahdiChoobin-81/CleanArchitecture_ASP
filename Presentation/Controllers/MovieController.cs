using Application.Dto.Entities;
using Application.Dto.Results;
using Application.Movies.Commands;
using Application.Movies.Query;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]

public class MovieController : Controller
{

    private ISender _sender;

    public MovieController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<ActionResult<MovieResultDto>> Post([FromBody] MovieDto input)
    {
        var command = new AddMovieCommand(
            input.MoiveName,
            input.MovieDescription,
            input.MovieRate,
            input.ReleaseDate,
            input.MainImage,
            input.Subtitle,
            input.Images,
            input.GenreIds
            );

        MovieResultDto result = await _sender.Send(command);

            switch (result.StatusCode)
            {
                case Movie_asp.StatusCode.BadRequest:
                    return BadRequest(result);
                case Movie_asp.StatusCode.NotFound:
                    return NotFound(result);
                default:
                    return Ok(result.Movie);
            }
    }


    [HttpGet]
    public async Task<ActionResult<MovieResultDto>> GetAll()
    {
        var movies = await _sender.Send(new GetAllMoviesQuery());
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<MovieResultDto>> GetById(Guid id)
    {
        var result = await _sender.Send(new GetMovieByIdQuery(new Id(id)));
        if (result.StatusCode == Movie_asp.StatusCode.NotFound)
            return NotFound(result);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<MovieResultDto>> Delete(Guid id)
    {
        var result = await _sender.Send(new DeleteMovieCommand(new Id(id)));

        if (result.StatusCode == Movie_asp.StatusCode.NotFound)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<MovieResultDto>> Put(Guid id, [FromBody] UpdateMovieDto movieDto)
    {
        var result = await _sender.Send(new UpdateMovieCommand(new Id(id), movieDto));
    
        if (result.StatusCode == Movie_asp.StatusCode.NotFound)
            return NotFound(result);
        
        if (result.StatusCode == Movie_asp.StatusCode.BadRequest)
            return BadRequest(result);

        return Ok(result);

    }
    
    
    
    
}