using Application.Data;

namespace Infrastructure;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly IApplicationDbContext _dbContext;
    
    public UnitOfWork(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _dbContext.SaveChangesAsync(cancellationToken);
    }


}