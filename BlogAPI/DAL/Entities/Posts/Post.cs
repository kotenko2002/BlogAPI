using BlogAPI.DAL.Entities.Categories;
using BlogAPI.DAL.Entities.Comments;
using BlogAPI.DAL.Entities.PostsHashtags;
using BlogAPI.DAL.Entities.Users;

namespace BlogAPI.DAL.Entities.Posts
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public ICollection<PostHashtag> PostHashtags { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
