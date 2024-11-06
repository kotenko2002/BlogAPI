using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.PostsHashtags;
using BlogAPI.DAL.Repositories.BaseRepository;

namespace BlogAPI.DAL.Repositories.PostsHashtags
{
    public class PostHashtagRepository : BaseRepository<PostHashtag>, IPostHashtagRepository
    {
        public PostHashtagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
