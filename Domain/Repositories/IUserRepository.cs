using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Movie_asp.Repositories;

public interface IUserRepository 
{
    void Add(User user);

    void Remove(User user);

    void Update(User user);

    Task<User?> GetByIdAsync(Id id);

    Task<IEnumerable<User>> GetAllAsync();

}