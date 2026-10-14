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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BlogDto>>> GetAllBlogs()
        {
            var blogs = await _blog.GetAllBlog();
            return Ok(blogs);
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
        public async Task<ActionResult<BlogDto>> CreateBlog([FromBody] CreateBlogDto blogDto)
        {
            var blog = await _blog.CreateBlog(blogDto);

            return CreatedAtAction(nameof(GetBlogById), new { id = blog.Id }, blog);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<BlogDto>> UpdateBlog(int id, [FromBody] UpdateBlogDto _updateBlog)
        {
            var updatedBlog = await _blog.UpdateBlog(id, _updateBlog);
            if (updatedBlog == null)
            {
                return NotFound();
            }
            return Ok(updatedBlog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blog.DeleteBlog(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent(); 
        }
    }
}
