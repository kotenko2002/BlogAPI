using BlogAPI.DAL.Entities.Categories;
using BlogAPI.DAL.Uow;

namespace BlogAPI.BLL.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _uow;

        public CategoryService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            IEnumerable<Category> categories = await _uow.CategoryRepository.FindAllAsync();
            return categories.ToList();
        }
    }
}
