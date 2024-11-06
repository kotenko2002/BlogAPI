using BlogAPI.BLL.Services.Hashtags;
using BlogAPI.DAL.Entities.Hashtags;
using BlogAPI.PL.Models.Hashtags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.PL.Controllers
{
    [ApiController, Route("[controller]")]
    public class HashtagController : ControllerBase
    {
        private readonly IHashtagService _hashtagService;

        public HashtagController(IHashtagService hashtagService)
        {
            _hashtagService = hashtagService;
        }

        [HttpGet("hashtags"), AllowAnonymous]
        public async Task<IActionResult> GetHashtags()
        {
            List<Hashtag> hashtags = await _hashtagService.GetAllHashtags();

            return Ok(hashtags.Select(h => new HashtagResponse(
                h.Id,
                h.Name
            )));
        }
    }
}
