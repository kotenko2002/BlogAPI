using BlogAPI.DAL.Repositories.Posts;

namespace BlogAPI.DAL.Uow
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }

        Task CompleteAsync();
    }
}
