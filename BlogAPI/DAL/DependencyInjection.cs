using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DAL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);

            return services;
        }

        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(configuration.GetConnectionString("ConnStr")));

            services.AddIdentity<User, IdentityRole<int>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            //services.AddIdentity<User, IdentityRole<Guid>>()
            //    .AddEntityFrameworkStores<CimasDbContext>()
            //    .AddDefaultTokenProviders()
            //    .AddErrorDescriber<UkrainianIdentityErrorDescriber>();

            return services;
        }
    }
}

