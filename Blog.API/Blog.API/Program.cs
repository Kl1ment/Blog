using Blog.API.Extentions;
using Blog.Application;
using Blog.DataAccess;
using Blog.Infrastucture;

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

services.AddReposytory(builder.Configuration);
services.AddApplication();
services.AddInfrastucture();


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
