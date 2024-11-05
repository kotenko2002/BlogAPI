using BlogAPI.BLL.Services.Posts;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.PL.Common.Extensions;
using BlogAPI.PL.Models.Posts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        // Приклад
        [HttpGet("public"), AllowAnonymous]
        public IActionResult Public()
        {
            return Ok();
        }

        // Приклад
        [HttpGet("private"), Authorize]
        public IActionResult Private()
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();

            return Ok($"Привіт, користувач з Id: {userId}");
        }

        [HttpPost("posts"), Authorize]
        public async Task<IActionResult> CreatePost(CreatePostRequest request)
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            await _postService.AddPostAsync(request, userId);

            return Ok($"Пост успішно створено");
        }

        [HttpGet("posts"), Authorize]
        public async Task<IActionResult> GetPostsByAuthorId(int authorId)
        {
            List<Post> posts = await _postService.GetPostsByAuthorIdAsync(authorId);

            return Ok(posts);
        }
    }
}
