using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Users;
using BlogAPI.DAL.Uow;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext(configuration)
                .AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<UkrainianIdentityErrorDescriber>();

            return services;
        }
    }
}

