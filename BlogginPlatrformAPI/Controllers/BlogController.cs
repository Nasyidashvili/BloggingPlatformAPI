using Microsoft.AspNetCore.Mvc;
using BlogginPlatformAPI.Data;
using BlogginPlatformAPI.Models;
using BlogginPlatformAPI.Interfaces;
using BlogginPlatformAPI.DTOS;

namespace BlogginPlatformAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {   
        private readonly IBlogService _blog;

        public BlogController(IBlogService blog)
        {
            _blog = blog;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogDto>> GetBlogById(int id)
        {
            var blog = await _blog.GetBlogById(id);
            if (blog == null)
            {
                return NotFound();
            }

            return Ok(blog);
        }

        [HttpPost]
        public async Task<ActionResult<BlogDto>> CreateBlog([FromBody]CreateBlogDto blogDto)
        {
            var blog = await _blog.CreateBlog(blogDto);

            return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
        }
    }
}
