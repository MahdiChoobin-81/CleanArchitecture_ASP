namespace Application.Data;

public interface IUnitOfWork
    // : IDisposable
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}