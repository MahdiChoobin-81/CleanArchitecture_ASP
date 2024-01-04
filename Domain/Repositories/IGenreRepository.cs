using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.Genre;
using Movie_asp.ValueObjects.User;

namespace Movie_asp.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    Task<Result> Add(Genre genre);

    void Remove(Genre genre);

    Task<Result> Update(Genre genre, GenreName currentGenreName);

    Task<IEnumerable<Genre>> GetAllAsync();
}