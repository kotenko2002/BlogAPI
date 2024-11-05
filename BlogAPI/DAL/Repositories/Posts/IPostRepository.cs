using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Repositories.BaseRepository;

namespace BlogAPI.DAL.Repositories.Posts
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<List<Post>> GetPostsByAuthorIdAsync(int authorId);
    }
}
