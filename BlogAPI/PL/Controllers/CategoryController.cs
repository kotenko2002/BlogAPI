using BlogAPI.BLL.Services.Categories;
using BlogAPI.DAL.Entities.Categories;
using BlogAPI.PL.Models.Categories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogAPI.PL.Controllers
{
    [ApiController, Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("categories"), AllowAnonymous]
        public async Task<IActionResult> GetCategories()
        {
            List<Category> categories = await _categoryService.GetAllCategories();

            return Ok(categories.Select(c => new CategoryResponse(
                c.Id,
                c.Name,
                c.Description
            )));
        }
    }
}
