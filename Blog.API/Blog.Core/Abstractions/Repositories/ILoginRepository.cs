using Blog.API.Models;

namespace Blog.DataAccess.Repositories
{
    public interface ILoginRepository
    {
        Task<List<LoginModel>> GetAllUsers();
        Task<LoginModel?> GetUserByEmail(string email);
        Task<int> CreateUser(LoginModel user);
        Task<int> DeleteUser(int id);
        Task<int> UpdateUser(int id, string email, string passwordHash);
    }
}