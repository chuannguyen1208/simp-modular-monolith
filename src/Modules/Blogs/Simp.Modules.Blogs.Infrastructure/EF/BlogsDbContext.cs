using Microsoft.EntityFrameworkCore;
using Simp.Modules.Blogs.Domain.Entities;

namespace Simp.Modules.Blogs.Infrastructure.EF;

public class BlogsDbContext : DbContext
{
    public BlogsDbContext(DbContextOptions<BlogsDbContext> options) : base(options)
    {
        
    }

    public BlogsDbContext()
    {
    }

    public DbSet<Blog> Blogs { get; set; }
}
