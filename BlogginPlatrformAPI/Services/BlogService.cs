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

        public async Task<List<BlogDto>> GetAllBlog()
        {
            var blog= await _context.Blogs
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
                .ToListAsync();
            return blog;
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

        public async Task<BlogDto> UpdateBlog(int id, UpdateBlogDto blogDto)
        {
            var blog = await _context.Blogs
                .Include(b => b.Tags)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blog == null)
            {
                return null;
            }

            blog.Title = blogDto.Title;
            blog.Category = blogDto.Category;
            blog.Content = blogDto.Content;
            blog.UpdatedAt = DateTime.UtcNow;

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

            blog.Tags = existingTags.Concat(newTags).ToList();
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

        public async Task<bool> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return false;
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<BlogDto>> GetBlogsByTitle(string? title)
        {
            var query = _context.Blogs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                title = title.Trim();
                query = query.Where(b => b.Title.Contains(title));
            }
            return await query.Select(b => new BlogDto
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                Category = b.Category,
                Tags = b.Tags.Select(t => t.Tags).ToList(),
                CreatedAt = b.CreatedAt,
                UpdatedAt = b.UpdatedAt
            }).ToListAsync();
        }
    }
}
