using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Domain.Entities;

namespace Simp.Modules.Blogs.Infrastructure.EF;

public class BlogsDbContext(DbContextOptions<BlogsDbContext> options) : DbContext(options)
{
    public DbSet<Blog> Blogs { get; set; }
}
