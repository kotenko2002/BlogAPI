using System.ComponentModel.DataAnnotations;

namespace BlogAPI.PL.Models.Posts
{
    public class UpdatePostRequest
    {
        [Required(ErrorMessage = "Ідентифікатор є обов'язковим")]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }

        public int? CategoryId { get; set; }

        public List<int> HashtagIds { get; set; }
    }
}
