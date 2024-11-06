using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Hashtags;
using BlogAPI.DAL.Repositories.BaseRepository;

namespace BlogAPI.DAL.Repositories.Hashtags
{
    public class HashtagRepository : BaseRepository<Hashtag>, IHashtagRepository
    {
        public HashtagRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
