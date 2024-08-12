using Blog.Infrastucture;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Blog.API.Extensions
{
    public static class ApiExtensions
    {

        public static void AddApiAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtOptions = configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.SecretKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            context.Token = context.Request.Cookies["asp"];

                            return Task.CompletedTask;
                        }
                    };
                });



            services.AddAuthorization();
        }

    }
}
