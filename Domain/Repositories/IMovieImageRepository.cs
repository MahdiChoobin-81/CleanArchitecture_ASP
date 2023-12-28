using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Movie_asp.Repositories;

public interface IMovieImageRepository
{
    Task<MovieImage?> FindByIdNameMovieId(Id id, Image image, Id movieId);
}