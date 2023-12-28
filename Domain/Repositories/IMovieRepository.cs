using Movie_asp.Dto;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Movie_asp.Repositories;

public interface IMovieRepository 
{
    void Add(Movie movie);

    void Remove(Movie movie);

    void Update(Movie movie);

    Task<Movie?> GetByIdAsync(Id id);

    Task<IEnumerable<Movie>> GetAllAsync();

}