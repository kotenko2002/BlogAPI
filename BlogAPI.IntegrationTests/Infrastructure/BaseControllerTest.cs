using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Data;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using BlogAPI.BLL.Services.Auth;
using BlogAPI.DAL.Entities.Users;
using BlogAPI.DAL.Common;
using BlogAPI.DAL.Entities.Categories;
using BlogAPI.DAL.Entities.Hashtags;
using BlogAPI.DAL.Entities.PostsHashtags;
using BlogAPI.DAL.Entities.Posts;
using Microsoft.AspNetCore.Http;

namespace BlogAPI.IntegrationTests.Infrastructure
{
    public class BaseControllerTest
    {
        private HttpClient _client;
        private WebApplicationFactory<Program> _factory;

        #region HardcodedInfo
        protected readonly string userName1 = "user1";
        protected readonly string userName2 = "user2";
        #endregion

        public async Task PerformTest(Func<HttpClient, Task> testFunc, Action<IServiceCollection> configureServices = null)
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    SetUpInMemoryDb(services);
                    configureServices?.Invoke(services);
                });
            });
            await SeedData();
            _client = _factory.CreateClient();

            await testFunc(_client);

            _client.Dispose();
            _factory.Dispose();
        }

        public async Task<string> GenerateTokenAndSetAsHeader(string username, bool setTikenAsHeader = true)
        {
            using var scope = _factory.Services.CreateScope();
            var config = scope.ServiceProvider.GetRequiredService<IOptions<JwtConfig>>().Value;
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            User user = await userManager.FindByNameAsync(username);

            var authClaims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            IList<string> userRoles = await userManager.GetRolesAsync(user);

            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.Secret));

            var accessToken = new JwtSecurityToken(
                issuer: config.ValidIssuer,
                audience: config.ValidAudience,
                expires: DateTime.Now.AddDays(config.TokenValidityInDays),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(accessToken);
            if (setTikenAsHeader)
            {
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return token;
        }

        public async Task<T> GetResponseContent<T>(HttpResponseMessage response)
        {
            string responseContent = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(responseContent);
        }

        public FormFile GetPhoto(string fileName)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string projectDirectory = Directory.GetParent(currentDirectory).Parent.Parent.FullName;
            string filePath = Path.Combine(projectDirectory, "Infrastructure", "Photos", fileName);

            string fileExtension = Path.GetExtension(fileName);
            string contentType = GetContentType(fileExtension);

            var stream = File.OpenRead(filePath);
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);

            return new FormFile(memoryStream, 0, memoryStream.Length, null, Path.GetFileName(stream.Name))
            {
                Headers = new HeaderDictionary(),
                ContentType = contentType
            };
        }

        private string GetContentType(string fileExtension)
        {
            switch (fileExtension.ToLower())
            {
                case ".jpg":
                case ".jpeg":
                    return "image/jpeg";
                case ".png":
                    return "image/png";
                case ".webp":
                    return "image/webp";
                default:
                    return "application/octet-stream";
            }
        }

        private void SetUpInMemoryDb(IServiceCollection services)
        {
            string databaseName = Guid.NewGuid().ToString();

            var dbContextDescriptor = services.SingleOrDefault(d =>
                d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
            services.Remove(dbContextDescriptor);
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName);
            });
        }

        private async Task SeedData()
        {
            using var scope = _factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            User user1 = new() { FirstName = "F1", LastName = "L1", UserName = userName1 };
            await userManager.CreateAsync(user1, "Qwerty123!");
            User user2 = new() { FirstName = "F2", LastName = "L2", UserName = userName2 };
            await userManager.CreateAsync(user2, "Qwerty123!");

            Category category1 = new() { Name = "C1", Description = "D1" };
            Category category2 = new() { Name = "C2", Description = "D2" };
            await context.Categories.AddRangeAsync(category1, category2);

            Hashtag hashtag1 = new() { Name = "H1" };
            Hashtag hashtag2 = new() { Name = "H2" };
            Hashtag hashtag3 = new() { Name = "H3" };
            Hashtag hashtag4 = new() { Name = "H4" };
            await context.Hashtags.AddRangeAsync(hashtag1, hashtag2, hashtag3, hashtag4);

            Post post1 = new() { Category = category1, Title = "T1", Description = "D1", Author = user1, PhotoFileName = "1.jpg", CreatedAt = DateTime.Now };
            Post post2 = new() { Category = category1, Title = "T2", Description = "D2", Author = user1, PhotoFileName = "2.jpg", CreatedAt = DateTime.Now };
            Post post3 = new() { Category = category2, Title = "T3", Description = "D3", Author = user1, PhotoFileName = "3.jpg", CreatedAt = DateTime.Now };
            await context.Posts.AddRangeAsync(post1, post2, post3);

            await context.PostsHashtags.AddRangeAsync([
                new PostHashtag() { Post = post1, Hashtag = hashtag1 },
                new PostHashtag() { Post = post1, Hashtag = hashtag2 },
                new PostHashtag() { Post = post2, Hashtag = hashtag1 },
                new PostHashtag() { Post = post3, Hashtag = hashtag2 },
                new PostHashtag() { Post = post3, Hashtag = hashtag3 },
                new PostHashtag() { Post = post3, Hashtag = hashtag4 }
            ]);

            await context.SaveChangesAsync();
        }
    }
}
