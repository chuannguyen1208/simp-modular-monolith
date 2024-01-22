using Microsoft.EntityFrameworkCore;
using Simp.Shared.Abstractions.Repositories;

namespace Simp.Shared.Infrastructure.Repositories;
public class UnitOfWork(DbContext dbContext) : IUnitOfWork
{
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        return new Repository<TEntity>(dbContext);
    }

    public int SaveChanges()
    {
        return dbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}
