using System.ComponentModel.DataAnnotations;

namespace BlogAPI.PL.Models.Posts
{
    public class CreatePostRequest
    {
        [Required(ErrorMessage = "Заголовок є обов'язковим")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Опис є обов'язковим")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Фото є обов'язковим")]
        public IFormFile Photo { get; set; }
        
        [Required(ErrorMessage = "Категорія є обов'язковою")]
        public int CategoryId { get; set; }

        public List<int> HashtagIds { get; set; }
    }
}
