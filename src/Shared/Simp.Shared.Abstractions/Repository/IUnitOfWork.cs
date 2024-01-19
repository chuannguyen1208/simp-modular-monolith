
namespace Simp.Shared.Abstractions.Repository;
public interface IUnitOfWork : IDisposable
{
    Task<int> SaveChangesAsync();
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
}
