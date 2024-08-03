using Blog.API.Models;

namespace Blog.Application.Services
{
    public interface ILoginService
    {
        Task<int> DeleteUser(int id);
        Task<bool> Login(string email, string password);
        Task<int> Register(LoginModel loginModel);
        Task<int> UpdateUser(int id, string email, string password);
    }
}