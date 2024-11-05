using BlogAPI.PL.Models.Auth;

namespace BlogAPI.BLL.Services.Auth
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest descriptor);
        Task<string> LoginAsync(LoginRequest descriptor);
    }
}
