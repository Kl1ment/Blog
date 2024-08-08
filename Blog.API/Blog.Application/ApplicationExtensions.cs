using Blog.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Application
{
    public static class ApplicationExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
        }

    }
}
