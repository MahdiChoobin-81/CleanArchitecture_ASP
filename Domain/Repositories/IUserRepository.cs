using Domain.Entities;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;

namespace Domain.Repositories;

public interface IUserRepository
{
    void Add(User user);

    Task<User?> GetByIdAsync(UserId id);
    
}