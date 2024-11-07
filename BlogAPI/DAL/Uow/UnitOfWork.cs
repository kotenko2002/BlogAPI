using BlogAPI.DAL.Common;
using BlogAPI.DAL.Repositories.Categories;
using BlogAPI.DAL.Repositories.Comments;
using BlogAPI.DAL.Repositories.Hashtags;
using BlogAPI.DAL.Repositories.Posts;
using BlogAPI.DAL.Repositories.PostsHashtags;

namespace BlogAPI.DAL.Uow
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IPostRepository PostRepository { get; }
        public IPostHashtagRepository PostHashtagRepository { get; }
        public IHashtagRepository HashtagRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public ICommentRepository CommentRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            PostRepository = new PostRepository(_context);
            PostHashtagRepository = new PostHashtagRepository(_context);
            HashtagRepository = new HashtagRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            CommentRepository = new CommentRepository(_context);
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
