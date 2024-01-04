using FluentResults;
using Movie_asp.Entities;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Movie_asp.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<Result> Add(User user);

    void Remove(User user);

    Task<Result> Update(User user, Email currentEmail, Username currentUsername);

    Task<IEnumerable<User>> GetAllAsync();


}