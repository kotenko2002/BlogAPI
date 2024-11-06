using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Categories;
using BlogAPI.DAL.Repositories.BaseRepository;

namespace BlogAPI.DAL.Repositories.Categories
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
