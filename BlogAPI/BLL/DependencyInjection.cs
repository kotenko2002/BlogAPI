using BlogAPI.BLL.Services.Auth;
using BlogAPI.BLL.Services.Categories;
using BlogAPI.BLL.Services.Comments;
using BlogAPI.BLL.Services.FileStorage;
using BlogAPI.BLL.Services.Hashtags;
using BlogAPI.BLL.Services.Posts;

namespace BlogAPI.BLL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddAuthService(configuration)
                .AddScoped<IFileStorage, FileStorage>()
                .AddScoped<IPostService, PostService>()
                .AddScoped<ICategoryService, CategoryService>()
                .AddScoped<IHashtagService, HashtagService>()
                .AddScoped<ICommentService, CommentService>();
        }

        public static IServiceCollection AddAuthService(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IAuthService, AuthService>()
                .Configure<JwtConfig>(configuration.GetSection("Jwt"));
        }
    }
}
