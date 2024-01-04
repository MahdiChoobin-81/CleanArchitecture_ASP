using Application.Dto.Results;
using Application.Validations;
using FluentResults;
using Movie_asp;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Application.Movies;

public static class FindDependentTableRecordsOfMovieTable<T>
{
    public static async Task<Result<List<T>?>> Find(
        List<Guid> tableRecordsIds,
        IRepository<T> tableRepository)
    {
        
        List<T> tableRecords = new List<T>();
        
        foreach (var tableRecordId in tableRecordsIds)
        {
            var result = await FindEntityRecordById<T>.Find(
                tableRepository, new Id(tableRecordId));
            if (result.IsFailed)
            {
                return result.ToResult();
            }

            tableRecords.Add(result.Value);
        }

        return Result.Ok(tableRecords);
    }
}