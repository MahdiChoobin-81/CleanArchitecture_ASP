using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Movie_asp.Repositories;

public interface IActorRepository : IRepository<Actor>
{
    Task<Result> Add(Actor actor);

    void Remove(Actor actor);

    Task<Result> Update(Actor actor, FullName currentFullName);

    Task<IEnumerable<Actor>> GetAllAsync();
}