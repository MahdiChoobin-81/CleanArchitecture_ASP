using Application.Data;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
using Movie_asp.Repositories;
using Movie_asp.ValueObjects;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;

    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
     
    }

    public void Remove(User user)
    {
        _context.Users.Remove(user);
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public Task<User?> GetByIdAsync(UserId id)
    {
        return _context.Users.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }
  
}