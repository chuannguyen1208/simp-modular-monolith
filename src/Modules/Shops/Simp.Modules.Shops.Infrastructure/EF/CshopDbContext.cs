using Microsoft.EntityFrameworkCore;
using Simp.Modules.Shops.Domain.Entities;

namespace Simp.Modules.Shops.Infrastructure.EF;
public class CshopDbContext(DbContextOptions<CshopDbContext> options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients { get; set; }
}
