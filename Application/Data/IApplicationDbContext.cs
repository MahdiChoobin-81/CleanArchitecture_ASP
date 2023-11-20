using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Movie_asp.Entities;

namespace Application.Data;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}