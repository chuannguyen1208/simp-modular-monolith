namespace Simp.Shared.Abstractions.Repositories;
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}
