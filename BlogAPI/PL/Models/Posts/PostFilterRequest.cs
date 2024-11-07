namespace BlogAPI.PL.Models.Posts
{
    public class PostFilterRequest
    {
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public List<int> HashtagIds { get; set; }
    }
}
