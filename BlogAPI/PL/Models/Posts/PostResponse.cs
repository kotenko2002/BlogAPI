using BlogAPI.PL.Models.Categories;
using BlogAPI.PL.Models.Hashtags;
using BlogAPI.PL.Models.Users;

namespace BlogAPI.PL.Models.Posts
{
    public record PostResponse(
        int Id,
        string Title,
        string Description,
        string PhotoUrl,
        UserResponse Author,
        CategoryResponse Category,
        List<HashtagResponse> Hashtags
    );
}
