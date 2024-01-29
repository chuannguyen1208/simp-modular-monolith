﻿namespace Simp.Shared.Abstractions.Repositories;
public interface IRepository<TEntity>
    where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(Guid id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
    IQueryable<TEntity> Entities { get; }
}
