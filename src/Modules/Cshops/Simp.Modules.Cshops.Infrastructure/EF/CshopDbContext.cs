using Microsoft.EntityFrameworkCore;
using Simp.Modules.Cshop.Domain.Entities;

namespace Simp.Modules.Cshops.Infrastructure.EF;
public class CshopDbContext(DbContextOptions<CshopDbContext> options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients { get; set; }
}
