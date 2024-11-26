using BlogAPI.DAL.Entities.Comments;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Uow;
using BlogAPI.PL.Common.Middlewares;
using BlogAPI.PL.Models.Comments;
using System.Net;

namespace BlogAPI.BLL.Services.Comments
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _uow;

        public CommentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task CreateCommentAsync(CreateCommentRequest request, int authorId, int postId)
        {
            Post post = await _uow.PostRepository.FindAsync(postId);
            if (post == null)
            {
                throw new BusinessException(HttpStatusCode.NotFound, $"Поста з ідентифікатором {postId} не існує");
            }

            if(request.ParentCommentId.HasValue)
            {
                Comment parentComment = await _uow.CommentRepository.FindAsync(request.ParentCommentId.Value);
                if (parentComment == null)
                {
                    throw new BusinessException(HttpStatusCode.NotFound, $"Батьківського коментаря з ідентифікатором {request.ParentCommentId.Value} не існує");
                }
            }

            var comment = new Comment()
            {
                Content = request.Content,
                AuthorId = authorId,
                PostId = postId,
                ParentCommentId = request.ParentCommentId,
            };

            await _uow.CommentRepository.AddAsync(comment);
            await _uow.CompleteAsync();
        }

        public async Task<List<Comment>> GetCommentsByPostIdAsync(int postId)
        {
            Post post = await _uow.PostRepository.FindAsync(postId);
            if (post == null)
            {
                throw new BusinessException(HttpStatusCode.NotFound, $"Поста з ідентифікатором {postId} не існує");
            }

            return await _uow.CommentRepository.GetcommentByPostIdAsync(postId);
        }
    }
}
