
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.PL.Models.Comments
{
    public class CreateCommentRequest
    {
        [Required(ErrorMessage = "Вміст коментаря є обов'язковим")]
        public string Content { get; set; }

        public int? ParentCommentId { get; set; }
    }
}
