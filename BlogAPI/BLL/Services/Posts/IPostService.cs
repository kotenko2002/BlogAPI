using BlogAPI.DAL.Entities.Posts;
using BlogAPI.PL.Models.Posts;

namespace BlogAPI.BLL.Services.Posts
{
    public interface IPostService
    {
        Task AddPostAsync(CreatePostRequest request, int authorId);
        Task UpdatePostAsync(UpdatePostRequest request, int currentUserId);
        Task<List<Post>> GetPostsByAuthorIdAsync(int authorId);
        Task<List<Post>> GetPostsByFilterAsync(PostFilterRequest request);
    }
}
