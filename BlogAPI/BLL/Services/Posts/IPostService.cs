using BlogAPI.DAL.Entities.Posts;
using BlogAPI.PL.Models.Posts;

namespace BlogAPI.BLL.Services.Posts
{
    public interface IPostService
    {
        Task AddPostAsync(CreatePostRequest request, int authorId);
        Task<List<Post>> GetPostsByAuthorIdAsync(int authorId);
    }
}
