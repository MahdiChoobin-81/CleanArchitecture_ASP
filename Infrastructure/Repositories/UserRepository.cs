using Application.Data;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;
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
    
    public Task<User?> GetByIdAsync(UserId id)
    {
        return _context.Users.SingleOrDefaultAsync(c => c.Id == id);
    }
}