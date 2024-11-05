using BlogAPI.DAL.Entities.Users;

namespace BlogAPI.DAL.Entities.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }
    }
}
