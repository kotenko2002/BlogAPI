using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Repositories.BaseRepository;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL.Repositories.Posts
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Post>> GetPostsByAuthorIdAsync(int authorId)
        {
            return await Sourse
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.PostHashtags)
                    .ThenInclude(ph => ph.Hashtag)
                .Where(p => p.AuthorId == authorId)
                .ToListAsync();
        }
    }
}
