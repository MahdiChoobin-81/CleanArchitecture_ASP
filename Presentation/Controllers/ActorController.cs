using Application.Data;
using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Presentation.Dto;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ActorController : Controller
{

    private readonly IActorRepository _actorRepository;
    private readonly IUnitOfWork _unitOfWork;


    public ActorController(IActorRepository actorRepository, IUnitOfWork unitOfWork)
    {
        _actorRepository = actorRepository;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomGenericResult>> GetById(Guid id)
    {
        var result = await FindEntityRecordById<Actor>.Find(
            _actorRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var actor = result.Value;
        return Result.Ok().ToCustomGenericResult(actor, Movie_asp.StatusCode.Ok);

    }


    [HttpGet]
    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        return await _actorRepository.GetAllAsync();
    }

    [HttpPost]
    public async Task<ActionResult<CustomGenericResult>> Post([FromBody] ActorDto input)
    {
        var fullName = FullName.Create(input.FullName);
        var img = Image.Create(input.Img);

        if (fullName.IsFailed)
        {
            return BadRequest(fullName.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        }

        if (img.IsFailed)
        {
            return BadRequest(img.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        }

        var actor = new Actor(
            new Id(Guid.NewGuid()),
            fullName.Value,
            img.Value);

        var result = await _actorRepository.Add(actor);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        await _unitOfWork.SaveChangesAsync();


        return CreatedAtAction(nameof(GetById), new{id = actor.Id.Value}, actor);

    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Delete(Guid id)
    {
        var result = await FindEntityRecordById<Actor>.Find(
            _actorRepository, new Id(id));

        if (result.IsFailed)
        {
            return result.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }

        var actor = result.Value;
        
        _actorRepository.Remove(actor);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(actor, Movie_asp.StatusCode.Ok);


    }



    [HttpPut("{id}")]
    public async Task<ActionResult<CustomGenericResult>> Put(Guid id, [FromBody] ActorDto input)
    {
        var findActor = await FindEntityRecordById<Actor>.Find(
            _actorRepository, new Id(id));

        if (findActor.IsFailed)
        {
            return findActor.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.NotFound);
        }
        var actor = findActor.Value;
        var currentActorName = actor.ActorName;
        var fullName = FullName.Create(input.FullName);
        var img = Image.Create(input.Img);
        

        if (fullName.IsFailed)
        {
            return BadRequest(fullName.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        }
        if (img.IsFailed)
        {
            return BadRequest(img.ToResult().ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest));
        }

        actor.Update(
            fullName.Value,
            img.Value);
        
        actor.Update(fullName.Value, img.Value);
        
        var result = await _actorRepository.Update(actor, currentActorName);
        if (result.IsFailed)
            return result.ToCustomGenericResult(null, Movie_asp.StatusCode.BadRequest);
        
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok().ToCustomGenericResult(actor, Movie_asp.StatusCode.Ok);

    }
    
    
}