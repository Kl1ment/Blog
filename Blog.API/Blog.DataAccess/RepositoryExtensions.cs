using Blog.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.DataAccess
{
    public static class ReposytoryExtensions
    {
        public static void AddReposytory(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BlogDbContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(nameof(BlogDbContext)));
            });

            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
        }
    }
}
