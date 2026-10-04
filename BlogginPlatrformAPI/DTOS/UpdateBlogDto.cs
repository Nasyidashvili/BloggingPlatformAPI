namespace BlogginPlatformAPI.DTOS
{
    public class UpdateBlogDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public List<string> Tags { get; set; }
    }
}
