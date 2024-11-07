using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Entities.Users;

namespace BlogAPI.DAL.Entities.Comments
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public int AuthorId { get; set; }
        public virtual User Author { get; set; }

        public int PostId { get; set; }
        public virtual Post Post { get; set; }

        public int? ParentCommentId { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; }
    }
}
