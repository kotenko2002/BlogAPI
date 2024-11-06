using BlogAPI.BLL.Services.Posts;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.PL.Common.Extensions;
using BlogAPI.PL.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogAPI.PL.Controllers
{
    [ApiController, Route("[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPostService _postService;

        public PostController(
            IHttpContextAccessor httpContextAccessor,
            IPostService postService
        )
        {
            _httpContextAccessor = httpContextAccessor;
            _postService = postService;
        }

        [HttpGet("posts/{authorId}"), AllowAnonymous]
        public async Task<IActionResult> GetPostsByAuthorId(int authorId)
        {
            List<Post> posts = await _postService.GetPostsByAuthorIdAsync(authorId);

            return Ok(posts.Select(p => new PostResponse(
                p.Id,
                p.Title,
                p.Description,
                new UserResponse(
                    p.Author.Id,
                    p.Author.FirstName,
                    p.Author.LastName
                )
            )));
        }

        [HttpPost("posts"), Authorize]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            await _postService.AddPostAsync(request, userId);

            return Ok($"Пост успішно створено");
        }


        [HttpGet("posts/mine"), Authorize]
        public async Task<IActionResult> GetMyPosts()
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            List<Post> posts = await _postService.GetPostsByAuthorIdAsync(userId);

            return Ok(posts.Select(p => new PostResponse(
                p.Id,
                p.Title,
                p.Description,
                new UserResponse(
                    p.Author.Id,
                    p.Author.FirstName,
                    p.Author.LastName
                )
            )));
        }
    }
}
