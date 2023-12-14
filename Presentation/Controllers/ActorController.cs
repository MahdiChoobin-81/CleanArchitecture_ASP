using Application.Data;
using Application.Dto.Results;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Presentation.Dto;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorController : Controller
{

    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IUnitOfWork _unitOfWork;


    public ActorController(IApplicationDbContext applicationDbContext, IUnitOfWork unitOfWork)
    {
        _applicationDbContext = applicationDbContext;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ActorResultDto>> GetById(Guid id)
    {
        var result = await _applicationDbContext.Actors
            .FirstOrDefaultAsync(a => a.Id == new Id(id));

        if (result is null)
        {
            return NotFound(Result.Fail("There's no Actor with This Id : " + id).ToActorResultDto(null));
        }

        return Ok(Result.Ok().ToActorResultDto(result));

    }


    [HttpGet]
    public async Task<ActionResult<ActorResultDto>> GetAllAsync()
    {
        var result = await _applicationDbContext.Actors.ToListAsync();

        return Ok(result);

    }

    [HttpPost]
    public async Task<ActionResult<ActorResultDto>> Post([FromBody] ActorDto input)
    {
        var fullName = FullName.Create(input.FullName);
        var img = Image.Create(input.Img);

        if (fullName.IsFailed)
        {
            return BadRequest(fullName.ToResult().ToActorResultDto(null));
        }

        if (img.IsFailed)
        {
            return BadRequest(img.ToResult().ToActorResultDto(null));
        }

        var actor = new Actor(
            new Id(Guid.NewGuid()),
            fullName.Value!,
            img.Value!);

        _applicationDbContext.Actors.Add(actor);
        await _unitOfWork.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new{id = actor.Id}, actor);

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<ActorResultDto>> Delete(Guid id)
    {
        var actor = await _applicationDbContext.Actors
            .FirstOrDefaultAsync(a => a.Id == new Id(id));

        if (actor is null)
        {
            return NotFound(Result.Fail("There's no Actor with This Id : " + id).ToActorResultDto(null));
        }

        _applicationDbContext.Actors.Remove(actor);
        await _unitOfWork.SaveChangesAsync();

        return Ok(actor);


    }



    [HttpPut("{id}")]
    public async Task<ActionResult<ActorResultDto>> Put(Guid id, [FromBody] ActorDto input)
    {
        var actor = await _applicationDbContext.Actors
            .FirstOrDefaultAsync(a => a.Id == new Id(id));
        
        if (actor is null)
        {
            return NotFound(Result.Fail("There's no Actor with This Id : " + id).ToActorResultDto(null));
        }

        var fullName = FullName.Create(input.FullName);
        var img = Image.Create(input.Img);

        if (fullName.IsFailed)
        {
            return BadRequest(fullName.ToResult().ToActorResultDto(null));
        }
        if (img.IsFailed)
        {
            return BadRequest(img.ToResult().ToActorResultDto(null));
        }
        
        actor.Update(fullName.Value!, img.Value!);

        _applicationDbContext.Actors.Update(actor);
        await _unitOfWork.SaveChangesAsync();
        return Ok(actor);

    }
    
    
}