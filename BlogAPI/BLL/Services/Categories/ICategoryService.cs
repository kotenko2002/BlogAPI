using BlogAPI.DAL.Entities.Categories;

namespace BlogAPI.BLL.Services.Categories
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
    }
}
