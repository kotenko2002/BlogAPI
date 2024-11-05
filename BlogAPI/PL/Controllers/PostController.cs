using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.PL.Controllers
{
    [ApiController, Route("[controller]")]
    public class PostController : ControllerBase
    {
        [HttpGet("public"), AllowAnonymous]
        public IActionResult Public()
        {
            return Ok();
        }

        [HttpGet("private"), Authorize]
        public IActionResult Private()
        {
            return Ok();
        }
    }
}
