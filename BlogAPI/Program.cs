
using BlogAPI.BLL;
using BlogAPI.DAL;
using BlogAPI.PL;

namespace BlogAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services
                .AddPresentationLayer(builder.Configuration)
                .AddBusinessLogicLayer(builder.Configuration)
                .AddDataAccessLayer(builder.Configuration);

            builder.Services
                .AddAuthScheme(builder.Configuration);

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
