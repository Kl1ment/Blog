using Blog.API.Models;
using CSharpFunctionalExtensions;

namespace Blog.Application.Services
{
    public interface ILoginService
    {
        Task<List<LoginModel>> GetAllUsers();
        Task<int> DeleteUser(int id);
        Task<IResult<string>> Login(string email, string password);
        Task<IResult<string>> Register(string email, string password);
        Task<int> UpdateUser(int id, string email, string password);
    }
}