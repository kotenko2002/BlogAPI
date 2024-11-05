using System.ComponentModel.DataAnnotations;

namespace BlogAPI.PL.Models.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Ім'я користувача є обов'язковим")]
        public string Username { get; set; }

        [EmailAddress, Required(ErrorMessage = "Електронна пошта є обов'язковою")]
        public string Email { get; set; }

        [Phone, Required(ErrorMessage = "Телефон є обов'язковим")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Пароль є обов'язковим")]
        public string Password { get; set; }

    }
}
