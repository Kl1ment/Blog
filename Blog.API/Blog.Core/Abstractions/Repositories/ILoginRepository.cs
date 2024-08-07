using Blog.API.Models;

namespace Blog.DataAccess.Repositories
{
    public interface ILoginRepository
    {
        Task<List<LoginModel>> GetAll();
        Task<LoginModel?> GetByEmail(string email);
        Task<int> Add(LoginModel user);
        Task<int> Delete(int id);
        Task<int> Update(int id, string email, string passwordHash);
    }
}