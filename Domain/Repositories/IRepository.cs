using Movie_asp.ValueObjects;

namespace Movie_asp.Repositories;

public interface IRepository<T>
{
    Task<T?> FindByIdAsync(Id id);
}