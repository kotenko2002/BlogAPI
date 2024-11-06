using BlogAPI.DAL.Repositories.Categories;
using BlogAPI.DAL.Repositories.Hashtags;
using BlogAPI.DAL.Repositories.Posts;
using BlogAPI.DAL.Repositories.PostsHashtags;

namespace BlogAPI.DAL.Uow
{
    public interface IUnitOfWork
    {
        IPostRepository PostRepository { get; }
        IPostHashtagRepository PostHashtagRepository { get; }
        IHashtagRepository HashtagRepository { get; }
        ICategoryRepository CategoryRepository { get; }

        Task CompleteAsync();
    }
}
