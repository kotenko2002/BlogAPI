using BlogAPI.BLL.Services.Auth;
using BlogAPI.BLL.Services.Posts;

namespace BlogAPI.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddAuthService(configuration)
                .AddScoped<IPostService, PostService>();
        }

        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IAuthService, AuthService>()
                .Configure<JwtConfig>(configuration.GetSection("Jwt"));
        }
    }
}
