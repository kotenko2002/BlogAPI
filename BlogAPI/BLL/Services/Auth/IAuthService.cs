using BlogAPI.PL.Models.Auth;

namespace BlogAPI.BLL.Services.Auth
{
    public interface IAuthService
    {
        Task RegisterAsync(RegisterRequest request);
        Task<string> LoginAsync(LoginRequest request);
    }
}
