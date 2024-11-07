using BlogAPI.DAL.Entities.Comments;
using BlogAPI.DAL.Repositories.BaseRepository;

namespace BlogAPI.DAL.Repositories.Comments
{
    public interface ICommentRepository : IBaseRepository<Comment>
    {
        Task<List<Comment>> GetcommentByPostIdAsync(int postId);
    }
}
