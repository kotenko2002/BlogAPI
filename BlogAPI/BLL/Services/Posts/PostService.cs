using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Uow;
using BlogAPI.PL.Models.Posts;

namespace BlogAPI.BLL.Services.Posts
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _uow;

        public PostService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task AddPostAsync(CreatePostRequest request, int authorId)
        {
            var newPost = new Post()
            {
                Title = request.Title,
                Description = request.Description,
                AuthorId = authorId
            };

            await _uow.PostRepository.AddAsync(newPost);
            await _uow.CompleteAsync();
        }

        public async Task<List<Post>> GetPostsByAuthorIdAsync(int authorId)
        {
            return await _uow.PostRepository.GetPostsByAuthorIdAsync(authorId);
        }
    }
}
