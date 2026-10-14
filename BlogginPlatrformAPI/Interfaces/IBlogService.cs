using BlogginPlatformAPI.Data;
using BlogginPlatformAPI.Interfaces;
using BlogginPlatformAPI.Models;
using BlogginPlatformAPI.DTOS;

namespace BlogginPlatformAPI.Interfaces
{
    public interface IBlogService
    {
        public Task<List<BlogDto>> GetAllBlog();
        public Task<BlogDto> CreateBlog(CreateBlogDto blogDto);
        public Task<BlogDto> GetBlogById(int id);
        public Task<BlogDto> UpdateBlog(int id, UpdateBlogDto blogDto);
        public Task<bool> DeleteBlog(int id);
        public Task<List<BlogDto>> GetBlogsByTitle(string? title);
    }
}
