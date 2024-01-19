using Microsoft.EntityFrameworkCore;
using Simp.Shared.Abstractions.Repository;

namespace Simp.Shared.Infrastructure.Repository;
internal class UnitOfWork(DbContext context) : IUnitOfWork
{
    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        return new Repository<TEntity>(context);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }

    public void Dispose()
    {
        context.Dispose();
    }
}
