using BlogginPlatformAPI.Data;
using BlogginPlatformAPI.Interfaces;
using BlogginPlatformAPI.Models;
using BlogginPlatformAPI.DTOS;
using Microsoft.EntityFrameworkCore;

namespace BlogginPlatformAPI.Services
{
    public class BlogService : IBlogService
    {
        private readonly BlogDbContext _context;

        public BlogService(BlogDbContext context)
        {
            _context = context;
        }

        public async Task<BlogDto> GetBlogById(int id)
        {
            var blog = await _context.Blogs
                .Where(b => b.Id == id)
                .Select(b => new BlogDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Content = b.Content,
                    Category = b.Category,
                    Tags = b.Tags.Select(t => t.Tags).ToList(),
                    CreatedAt = b.CreatedAt,
                    UpdatedAt = b.UpdatedAt
                })
                .FirstOrDefaultAsync();
            return blog;
        }

        public async Task<BlogDto> CreateBlog(CreateBlogDto blogDto)
        {
            var names = blogDto.Tags
                .Select(t => t.Trim())
                .Distinct()
                .ToList();

            var existingTags = await _context.Tags
                .Where(t => names.Contains(t.Tags))
                .ToListAsync();

            var newTags = names
                .Where(n => !existingTags.Any(t => t.Tags.Equals(n)))
                .Select(n => new Tag { Tags = n })
                .ToList();

            var dateTimeNow = DateTime.UtcNow;

            var blog = new Blog
            {
                Title = blogDto.Title,
                Content = blogDto.Content,
                Category = blogDto.Category,
                Tags = existingTags.Concat(newTags).ToList(),
                CreatedAt = dateTimeNow,
                UpdatedAt = dateTimeNow
            };

            await _context.Blogs.AddAsync(blog);
            await _context.SaveChangesAsync();

            return new BlogDto
            {
                Id = blog.Id,
                Title = blog.Title,
                Content = blog.Content,
                Category = blog.Category,
                Tags = blog.Tags.Select(t => t.Tags).ToList(),
                CreatedAt = blog.CreatedAt,
                UpdatedAt = blog.UpdatedAt
            };
        }

    }
}
