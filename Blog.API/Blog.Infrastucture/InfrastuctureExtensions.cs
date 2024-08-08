using Microsoft.Extensions.DependencyInjection;

namespace Blog.Infrastucture
{
    public static class InfrastuctureExtensions
    {
        public static void AddInfrastucture(this IServiceCollection services)
        {
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IJwtProvider, JwtProvider>();
        }
    }
}
