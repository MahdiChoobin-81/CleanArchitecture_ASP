using System.Collections;
using Application.Dto.Results;
using FluentResults;
using Movie_asp.Entities;
using Movie_asp.Repositories;

namespace Application.Validations;

public static class MovieListsValidation<T>
{
    public static async Task<Result> Find(ICollection<T> domainLists, IRepository<T> repository)
    {
        foreach (IDomain domainList in domainLists)
        {
            var isFound = await repository.FindByIdAsync(domainList.Id);
            if (isFound == null){
                return Result.Fail("There's no "
                                   +nameof(T)+
                                   " with This Id : " + domainList.Id);
            }
        }

        return Result.Ok();
    }
}
