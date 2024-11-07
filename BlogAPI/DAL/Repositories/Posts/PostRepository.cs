using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Repositories.BaseRepository;
using BlogAPI.PL.Models.Posts;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL.Repositories.Posts
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override async Task<Post> FindAsync(int postId)
        {
            return await Source
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.PostHashtags)
                    .ThenInclude(ph => ph.Hashtag)
                .FirstOrDefaultAsync(p => p.Id == postId);
        }

        public async Task<List<Post>> GetPostsByAuthorIdAsync(int authorId)
        {
            return await Source
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.PostHashtags)
                    .ThenInclude(ph => ph.Hashtag)
                .Where(p => p.AuthorId == authorId)
                .ToListAsync();
        }

        public async Task<List<Post>> GetPostsByFilterAsync(PostFilterRequest request)
        {
            var query = Source
                .Include(p => p.Author)
                .Include(p => p.Category)
                .Include(p => p.PostHashtags)
                    .ThenInclude(ph => ph.Hashtag)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(p => p.Title.Contains(request.Title));
            }

            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == request.CategoryId.Value);
            }

            if (request.HashtagIds != null && request.HashtagIds.Any())
            {
                query = query.Where(p => p.PostHashtags.Any(ph => request.HashtagIds.Contains(ph.HashtagId)));
            }

            return await query.ToListAsync();
        }
    }
}
