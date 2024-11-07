using BlogAPI.DAL.Entities.Comments;
using BlogAPI.PL.Models.Comments;

namespace BlogAPI.BLL.Services.Comments
{
    public interface ICommentService
    {
        Task<List<Comment>> GetCommentsByPostIdAsync(int  postId);
        Task CreateCommentAsync(CreateCommentRequest request, int authorId, int postId);
    }
}
