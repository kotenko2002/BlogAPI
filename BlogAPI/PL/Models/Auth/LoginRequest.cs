using System.ComponentModel.DataAnnotations;

namespace BlogAPI.PL.Models.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Ім'я користувача є обов'язковим")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим")]
        public string Password { get; set; }
    }
}
