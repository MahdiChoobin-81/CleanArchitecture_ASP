using Application.Data;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;
using Movie_asp.ValueObjects.User;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;

    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Add(User user)
    {
        IQueryable<User> users = _context.Users.AsQueryable();

        var isEmailUnique = await users
            .Where(u => u.Email == user.Email).FirstOrDefaultAsync();
        
        if (isEmailUnique is not null)
        {
            return Result.Fail("Email already exist.");
        }
        
        var isUsernameUnique = await users
            .Where(u => u.Username == user.Username).FirstOrDefaultAsync();

        if (isUsernameUnique is not null)
        {
            return Result.Fail("Username already exist.");
        }
        
        _context.Users.Add(user);
        return Result.Ok();

    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }

    public async Task<Result> Update(User user, Email currentEmail, Username currentUsername)
    {

            IQueryable<User> users = _context.Users.AsQueryable();

            var isEmailUnique = await users
                .Where(u =>
                    u.Email != currentEmail &&
                    u.Email == user.Email)
                .FirstOrDefaultAsync();
            
            if (isEmailUnique is not null)
            {
                return Result.Fail("Email already exist.");
            }

            var isUsernameUnique = await users
                .Where(u =>
                    u.Username != currentUsername &&
                    u.Username == user.Username)
                .FirstOrDefaultAsync();
            
            
            if (isUsernameUnique is not null)
            {
                return Result.Fail("Username already exist.");
            }

            _context.Users.Update(user);
            return Result.Ok();
    }

    public Task<User?> FindByIdAsync(Id id)
    {
        return _context.Users.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }


}