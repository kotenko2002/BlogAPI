using BlogAPI.DAL.Entities.Hashtags;

namespace BlogAPI.BLL.Services.Hashtags
{
    public interface IHashtagService
    {
        Task<List<Hashtag>> GetAllHashtags();
    }
}
