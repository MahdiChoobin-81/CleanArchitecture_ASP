using FluentResults;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Application.Validations;

public static class FindEntityRecordById<T>
{
    public static async Task<Result<T>> Find(
        IRepository<T> repository,
        Id id)
    {
        
        var result = await repository.FindByIdAsync(id);
        
        if (result is null)
        {
            return Result.Fail("There's no "+typeof(T).Name+" with this Id : " + id.Value);
        }
        
        return Result.Ok(result);
    }
}