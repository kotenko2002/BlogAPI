using BlogAPI.BLL.Services.Comments;
using BlogAPI.DAL.Entities.Comments;
using BlogAPI.PL.Common.Extensions;
using BlogAPI.PL.Models.Comments;
using BlogAPI.PL.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogAPI.PL.Controllers
{
    [ApiController, Route("[controller]")]
    public class CommentController: ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService, IHttpContextAccessor httpContextAccessor)
        {
            _commentService = commentService;
            _httpContextAccessor = httpContextAccessor;

        }

        [HttpGet("comments/{postId}"), AllowAnonymous]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            List<Comment> comments = await _commentService.GetCommentsByPostIdAsync(postId);

            var response = comments.Select(c => new CommentResponse(
                c.Id,
                c.Content,
                c.CreatedAt,
                new UserResponse(
                    c.Author.Id,
                    c.Author.FirstName,
                    c.Author.LastName
                ),
                c.Replies.Select(r => new ReplyResponse(
                    r.Id,
                    r.Content,
                    r.CreatedAt,
                    new UserResponse(
                        r.Author.Id,
                        r.Author.FirstName,
                        r.Author.LastName
                    )
                )).ToList()
            ));

            return Ok(response);
        }

        [HttpPost("comments/{postId}"), Authorize]
        public async Task<IActionResult> CreateComment(int postId, CreateCommentRequest request)
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();

            await _commentService.CreateCommentAsync(request, userId, postId);

            return Ok($"Коментар успішно створено");
        }
    }
}
