using Application.Data;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Infrastructure.Repositories;

public class ActorRepository : IActorRepository
{
    private readonly IApplicationDbContext _context;

    public ActorRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Actor?> FindByIdAsync(Id id)
    {
        return await _context.Actors.FirstOrDefaultAsync(g => g.Id == id);
    }


    public async Task<Result> Add(Actor actor)
    {
        IQueryable<Actor> actors = _context.Actors.AsQueryable();

        var isActorNameUnique = await actors
            .Where(g => g.ActorName == actor.ActorName).FirstOrDefaultAsync();
        
        if (isActorNameUnique is not null)
        {
            return Result.Fail("Actor Name already exist.");
        }
        _context.Actors.Add(actor);
        return Result.Ok();
    }

    public void Remove(Actor actor)
    {
        _context.Actors.Remove(actor);
    }

    public async Task<Result> Update(Actor actor, FullName currentActorName)
    {
        IQueryable<Actor> actors = _context.Actors.AsQueryable();

        var isActorNameUnique = await actors
            .Where(g =>
                g.ActorName != currentActorName &&
                g.ActorName == actor.ActorName)
            .FirstOrDefaultAsync();
            
        if (isActorNameUnique is not null)
        {
            return Result.Fail("Actor Name already exist.");
        }
        
        _context.Actors.Update(actor);
        return Result.Ok();
    }

    public async Task<IEnumerable<Actor>> GetAllAsync()
    {
        return await _context.Actors.ToListAsync();
    }
}