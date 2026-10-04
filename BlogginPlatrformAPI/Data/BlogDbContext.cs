using Microsoft.EntityFrameworkCore;
using BlogginPlatformAPI.Models;

namespace BlogginPlatformAPI.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options)
        {

        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
