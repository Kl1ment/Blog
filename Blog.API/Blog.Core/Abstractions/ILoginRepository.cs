using Blog.API.Models;

namespace Blog.DataAccess.Repositories
{
    public interface ILoginRepository
    {
        Task<LoginModel?> GetUser(string email);
        Task<int> CreateUser(LoginModel user);
        Task<int> DeleteUser(int id);
        Task<int> UpdateUser(int id, string email, int hashPassword);
    }
}