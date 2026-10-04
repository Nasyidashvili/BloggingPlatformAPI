using BlogginPlatformAPI.Models;

namespace BlogginPlatformAPI.DTOS
{
    public class CreateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
    }
}
