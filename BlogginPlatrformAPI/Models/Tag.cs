namespace BlogginPlatformAPI.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Tags { get; set; }
        public List<Blog> Blogs { get; set; }
    }
}
