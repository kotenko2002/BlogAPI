using BlogAPI.DAL.Entities.Comments;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.DAL.Entities.Users;
using BlogAPI.PL.Models.Users;

namespace BlogAPI.PL.Models.Comments
{
    record CommentResponse(
        int Id,
        string Content,
        DateTime CreatedAt,
        UserResponse Author,
        List<ReplyResponse> Replies
    );

    record ReplyResponse(
        int Id,
        string Content,
        DateTime CreatedAt,
        UserResponse Author
    );
}
