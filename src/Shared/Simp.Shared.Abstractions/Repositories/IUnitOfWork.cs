namespace Simp.Shared.Abstractions.Repositories;
public interface IUnitOfWork
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    int SaveChanges();
    Task<int> SaveChangesAsync();
}
