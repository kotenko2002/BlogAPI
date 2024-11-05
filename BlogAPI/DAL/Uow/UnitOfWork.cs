using BlogAPI.DAL.Common;
using BlogAPI.DAL.Repositories.Posts;

namespace BlogAPI.DAL.Uow
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IPostRepository PostRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            PostRepository = new PostRepository(_context);
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
