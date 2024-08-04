using Blog.API.Models;

namespace Blog.Infrastucture
{
    public interface IJwtProvider
    {
        string GenerateToken(LoginModel loginModel);
    }
}