using BlogAPI.DAL.Entities.Hashtags;
using BlogAPI.DAL.Uow;

namespace BlogAPI.BLL.Services.Hashtags
{
    public class HashtagService : IHashtagService
    {
        private readonly IUnitOfWork _uow;

        public HashtagService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<Hashtag>> GetAllHashtags()
        {
            IEnumerable<Hashtag> hashtags = await _uow.HashtagRepository.FindAllAsync();
            return hashtags.ToList();
        }
    }
}
