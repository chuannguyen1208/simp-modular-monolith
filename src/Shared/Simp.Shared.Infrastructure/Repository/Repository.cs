using Microsoft.EntityFrameworkCore;
using Simp.Shared.Abstractions.Repository;

namespace Simp.Shared.Infrastructure.Repository;
internal class Repository<TEntity>(DbContext context) : IRepository<TEntity> 
    where TEntity : class
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task AddAsync(TEntity entity)
    {
        _dbSet.Add(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity);
        }
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }
}
