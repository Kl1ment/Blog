using Blog.API.Extentions;
using Blog.Application.Services;
using Blog.DataAccess;
using Blog.DataAccess.Repositories;
using Blog.Infrastucture;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
// Add services to the container.

services.AddApiAuthentication(builder.Configuration);

services.Configure<JwtOptions>(
    builder.Configuration.GetSection(nameof(JwtOptions)));

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<BlogDbContext>(
    options =>
    {
        options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(BlogDbContext)));
    });

services.AddScoped<ILoginService, LoginService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IPostService, PostService>();

services.AddScoped<ILoginRepository, LoginRepository>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IPostRepository, PostRepository>();

services.AddScoped<IPasswordHasher, PasswordHasher>();
services.AddScoped<IJwtProvider, JwtProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
