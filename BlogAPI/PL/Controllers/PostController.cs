using BlogAPI.BLL.Services.Posts;
using BlogAPI.DAL.Entities.Posts;
using BlogAPI.PL.Common.Extensions;
using BlogAPI.PL.Models.Categories;
using BlogAPI.PL.Models.Hashtags;
using BlogAPI.PL.Models.Posts;
using BlogAPI.PL.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.PL.Controllers
{
    [ApiController, Route("posts")]
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


        [HttpPost("filter"), AllowAnonymous]
        public async Task<IActionResult> GetPostsByFilter(PostFilterRequest request)
        {
            List<Post> posts = await _postService.GetPostsByFilterAsync(request);

            return Ok(ConvertPostsToResponseDto(posts));
        }

        [HttpGet("{authorId}"), AllowAnonymous]
        public async Task<IActionResult> GetPostsByAuthorId(int authorId)
        {
            List<Post> posts = await _postService.GetPostsByAuthorIdAsync(authorId);

            return Ok(ConvertPostsToResponseDto(posts));
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostRequest request)
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            await _postService.AddPostAsync(request, userId);

            return Ok($"Пост успішно створено");
        }

        [HttpPatch, Authorize]
        public async Task<IActionResult> UpdatePost([FromForm] UpdatePostRequest request)
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            await _postService.UpdatePostAsync(request, userId);

            return Ok($"Пост успішно оновлено");
        }

        [HttpGet("mine"), Authorize]
        public async Task<IActionResult> GetMyPosts()
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            List<Post> posts = await _postService.GetPostsByAuthorIdAsync(userId);

            return Ok(ConvertPostsToResponseDto(posts));
        }

        [HttpDelete("{postId}"), Authorize]
        public async Task<IActionResult> DeletePost(int postId)
        {
            int userId = _httpContextAccessor.HttpContext.User.GetUserId();
            await _postService.DeletePostAsync(postId, userId);

            return Ok("Пост успішно видалено");
        }

        private List<PostResponse> ConvertPostsToResponseDto(List<Post> posts)
        {
            return posts.Select(p => new PostResponse(
                p.Id,
                p.Title,
                p.Description,
                $"{Request.Scheme}://{Request.Host}/uploads/{p.PhotoFileName}",
                new UserResponse(
                    p.Author.Id,
                    p.Author.FirstName,
                    p.Author.LastName
                ),
                new CategoryResponse(
                    p.Category.Id,
                    p.Category.Name,
                    p.Category.Description
                ),
                p.PostHashtags.Select(ph => new HashtagResponse(
                    ph.Hashtag.Id,
                    ph.Hashtag.Name
                )).ToList()
            )).ToList();
        }
    }
}
