using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Repositories.BaseRepository;
using BlogAPI.PL.Models.Posts;

namespace BlogAPI.DAL.Repositories.Posts
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<List<Post>> GetPostsByAuthorIdAsync(int authorId);
        Task<List<Post>> GetPostsByFilterAsync(PostFilterRequest request);
    }
}
