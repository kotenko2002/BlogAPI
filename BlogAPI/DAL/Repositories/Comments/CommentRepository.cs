using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Comments;
using BlogAPI.DAL.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL.Repositories.Comments
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Comment>> GetcommentByPostIdAsync(int postId)
        {
            return await Source
                .Include(c => c.Replies)
                .Where(c => c.PostId == postId && !c.ParentCommentId.HasValue)
                .ToListAsync();
        }
    }
}
